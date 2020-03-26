using System;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using IdentityIssuer.Application.Auth.Commands;
using IdentityIssuer.Application.Configuration;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants.Commands;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Application.Users.Commands;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IdentityIssuer.Persistence.Utilities
{
    public class TenantInitializer : ITenantInitializer
    {
        private readonly IAdminTenantConfiguration _adminTenantConfiguration;
        private readonly ITenantsRepository _tenantsRepository;
        private readonly IMediator _mediator;
        private readonly IGuid _guid;
        private readonly IMapper _mapper;
        private readonly ILogger<TenantInitializer> _logger;

        public TenantInitializer(
            IAdminTenantConfiguration adminTenantConfiguration,
            ITenantsRepository tenantsRepository,
            IMediator mediator,
            IGuid guid,
            IMapper mapper,
            ILogger<TenantInitializer> logger)
        {
            _adminTenantConfiguration = adminTenantConfiguration;
            _tenantsRepository = tenantsRepository;
            _mediator = mediator;
            _guid = guid;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TenantDto> InitializeTenantFromConfigurationAsync()
        {
            var config = _adminTenantConfiguration;
            try
            {
                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var tenant = await _tenantsRepository.GetTenantAsync(config.Code);
                    var adminContextData = new AdminContextData(AdminContextType.System);

                    if (tenant != null)
                        return _mapper.Map<TenantDto>(tenant);

                    var createTenantCommand = new CreateTenantCommand(
                        config.Name,
                        config.Code,
                        config.AllowedOrigin,
                        config.TokenSecret, 
                        config.TokenExpirationInMinutes,
                        true,
                        false, 
                        false,
                        adminContextData);

                    var createTenantResult = await _mediator.Send(createTenantCommand);
                    if (!createTenantResult.IsSuccess)
                        throw new DomainException(createTenantResult);

                    tenant = await _tenantsRepository.GetTenantAsync(config.Code);

                    var createUserCommand = new RegisterUserWithCredentialsCommand(
                            _guid.GetGuid(),
                            config.Email,
                            config.UserDisplayName,
                            config.Password,
                            new TenantContextData(tenant.Id, tenant.Code))
                        .WithRequestGuid(createTenantCommand.RequestGuid);

                    var createUserResult = await _mediator.Send(createUserCommand);
                    if (!createUserResult.IsSuccess)
                        throw new DomainException(createTenantResult);

                    var makeUserAdminCommand = new MakeUserAdminCommand(
                            createUserResult.Data.UserGuid,
                            adminContextData)
                        .WithRequestGuid(createTenantCommand.RequestGuid);

                    var makeUserAdminResult = await _mediator.Send(makeUserAdminCommand);
                    if (!makeUserAdminResult.IsSuccess)
                        throw new DomainException(makeUserAdminResult);

                    transactionScope.Complete();
                    return _mapper.Map<TenantDto>(tenant);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }
    }
}