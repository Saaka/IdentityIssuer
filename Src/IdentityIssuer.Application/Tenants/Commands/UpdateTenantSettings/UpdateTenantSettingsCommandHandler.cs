using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands.UpdateTenantSettings
{
    public class UpdateTenantSettingsCommandHandler : RequestHandler<UpdateTenantSettingsCommand, TenantSettingsDto>
    {
        private readonly ITenantsRepository _tenantsRepository;
        private readonly ITenantSettingsRepository _tenantSettingsRepository;
        private readonly IMapper _mapper;

        public UpdateTenantSettingsCommandHandler(
            ITenantsRepository tenantsRepository,
            ITenantSettingsRepository tenantSettingsRepository,
            IMapper mapper)
        {
            _tenantsRepository = tenantsRepository;
            _tenantSettingsRepository = tenantSettingsRepository;
            _mapper = mapper;
        }
        
        public override async Task<RequestResult<TenantSettingsDto>> Handle(
            UpdateTenantSettingsCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantsRepository.GetTenantAsync(request.TenantCode);
            if (tenant == null)
                return RequestResult<TenantSettingsDto>.Failure(ErrorCode.TenantNotFound, 
                    new {Code = request.TenantCode});

            var saveData = _mapper.Map<SaveTenantSettingsData>(request);
            saveData.TenantId = tenant.Id;
            var tenantSettings = await _tenantSettingsRepository.SaveTenantSettings(saveData);
            if(tenantSettings == null)
                return RequestResult<TenantSettingsDto>.Failure(ErrorCode.UpdateTenantSettingsFailed);

            return RequestResult<TenantSettingsDto>
                .Success(_mapper.Map<TenantSettingsDto>(tenantSettings));
        }
    }
}