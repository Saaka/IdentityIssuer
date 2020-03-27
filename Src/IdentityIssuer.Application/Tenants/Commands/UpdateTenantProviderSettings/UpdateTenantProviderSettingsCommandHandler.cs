using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands.UpdateTenantProviderSettings
{
    public class UpdateTenantProviderSettingsCommandHandler 
        : RequestHandler<UpdateTenantProviderSettingsCommand, TenantProviderSettingsDto>
    {
        private readonly ITenantProviderSettingsRepository _providerSettingsRepository;
        private readonly ITenantProvider _tenantProvider;
        private readonly IMapper _mapper;

        public UpdateTenantProviderSettingsCommandHandler(
            ITenantProviderSettingsRepository providerSettingsRepository,
            ITenantProvider tenantProvider,
            IMapper mapper)
        {
            _providerSettingsRepository = providerSettingsRepository;
            _tenantProvider = tenantProvider;
            _mapper = mapper;
        }

        public override async Task<RequestResult<TenantProviderSettingsDto>> Handle(
            UpdateTenantProviderSettingsCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantProvider.GetTenantAsync(request.TenantCode);
            if (!await _providerSettingsRepository.ProviderSettingsExistsAsync(tenant.Id, request.ProviderType))
                return RequestResult<TenantProviderSettingsDto>
                    .Failure(ErrorCode.TenantProviderSettingsNotFound);

            var updateData = _mapper.Map<UpdateTenantProviderSettingsData>(request);
            updateData.TenantId = tenant.Id;
            var providerSettings = await _providerSettingsRepository
                .UpdateTenantProviderSettings(updateData);
            
            if(providerSettings == null)
                return RequestResult<TenantProviderSettingsDto>
                    .Failure(ErrorCode.UpdateTenantProviderSettingsFailed);
            
            return RequestResult<TenantProviderSettingsDto>
                .Success(_mapper.Map<TenantProviderSettingsDto>(providerSettings));
        }
    }
}