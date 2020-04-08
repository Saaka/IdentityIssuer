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
using IdentityIssuer.Application.Users.Models;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;
using IdentityIssuer.Common.Requests;
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

                    var correlationId = await CreateTenant(config, adminContextData);

                    tenant = await _tenantsRepository.GetTenantAsync(config.Code);

                    var createUserResult = await CreateUser(config, tenant, correlationId);

                    await MakeUserAdmin(createUserResult.Data.UserGuid, adminContextData, correlationId);

                    await MakeUserTenantOwner(createUserResult.Data.UserGuid, adminContextData, correlationId);

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

        private async Task<Guid> CreateTenant(IAdminTenantConfiguration config, AdminContextData adminContextData)
        {
            var createTenantCommand = new CreateTenantCommand(
                    config.Name,
                    config.Code,
                    config.AllowedOrigin,
                    config.TokenSecret,
                    config.TokenExpirationInMinutes,
                    true,
                    false,
                    false)
                .WithAdminContextData(adminContextData);

            var createTenantResult = await _mediator.Send(createTenantCommand);
            if (!createTenantResult.IsSuccess)
                throw new DomainException(createTenantResult);
            return createTenantCommand.CorrelationId;
        }

        private async Task<RequestResult<UserDto>> CreateUser(
            IAdminTenantConfiguration config, Tenant tenant, Guid correlationId)
        {
            var createUserCommand = new RegisterUserWithCredentialsCommand(
                    _guid.GetGuid(),
                    config.Email,
                    config.UserDisplayName,
                    config.Password,
                    new TenantContextData(tenant.Id, tenant.Code))
                .WithCorrelationId(correlationId);

            var createUserResult = await _mediator.Send(createUserCommand);
            if (!createUserResult.IsSuccess)
                throw new DomainException(createUserResult);
            return createUserResult;
        }

        private async Task MakeUserAdmin(Guid userGuid, AdminContextData adminContextData, Guid correlationId)
        {
            var makeUserAdminCommand = new MakeUserAdminCommand(
                    userGuid,
                    adminContextData)
                .WithCorrelationId(correlationId);

            var makeUserAdminResult = await _mediator.Send(makeUserAdminCommand);
            if (!makeUserAdminResult.IsSuccess)
                throw new DomainException(makeUserAdminResult);
        }

        private async Task MakeUserTenantOwner(Guid userGuid, AdminContextData adminContextData, Guid correlationId)
        {
            var makeUserOwnerCommand = new MakeUserOwnerCommand(
                    userGuid,
                    adminContextData)
                .WithCorrelationId(correlationId);

            var makeUserOwnerResult = await _mediator.Send(makeUserOwnerCommand);
            if (!makeUserOwnerResult.IsSuccess)
                throw new DomainException(makeUserOwnerResult);
        }
    }
}