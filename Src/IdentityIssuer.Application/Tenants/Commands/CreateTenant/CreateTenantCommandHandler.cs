using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Application.Users.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands.CreateTenant
{
    public class CreateTenantCommandHandler : RequestHandler<CreateTenantCommand, TenantDto>
    {
        private readonly ITenantsRepository _tenantsRepository;
        private readonly ITenantSettingsRepository _tenantSettingsRepository;
        private readonly ITenantAllowedOriginsRepository _tenantAllowedOriginsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public CreateTenantCommandHandler(
            ITenantsRepository tenantsRepository,
            ITenantSettingsRepository tenantSettingsRepository,
            ITenantAllowedOriginsRepository tenantAllowedOriginsRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _tenantsRepository = tenantsRepository;
            _tenantSettingsRepository = tenantSettingsRepository;
            _tenantAllowedOriginsRepository = tenantAllowedOriginsRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public override async Task<RequestResult<TenantDto>> Handle(CreateTenantCommand request,
            CancellationToken cancellationToken)
        {
            if (await _tenantsRepository.TenantCodeExists(request.Code))
                throw new DomainException(ErrorCode.TenantAlreadyExistsForCode, new {Code = request.Code});

            var tenant = await _tenantsRepository
                .CreateTenant(new CreateTenantDto(request.Name, request.Code, request.AllowedOrigin));

            if (tenant == null)
                return RequestResult<TenantDto>.Failure(ErrorCode.CreateTenantFailed);

            var tenantSettings = await _tenantSettingsRepository
                .CreateTenantSettings(new CreateTenantSettingsDto(
                    tenant.Id,
                    request.TokenSecret,
                    request.TokenExpirationInMinutes,
                    enableCredentialsLogin: true,
                    enableGoogleLogin: false,
                    enableFacebookLogin: false));

            if (tenantSettings == null)
                return RequestResult<TenantDto>.Failure(ErrorCode.CreateTenantFailed);

            if(!await _tenantAllowedOriginsRepository
                .SaveAllowedOriginsAsync(tenant.Id, new List<string>{ request.AllowedOrigin}))
                return RequestResult<TenantDto>.Failure(ErrorCode.CreateTenantFailed);

            return RequestResult<TenantDto>
                .Success(_mapper.Map<TenantDto>(tenant));
        }
    }
}