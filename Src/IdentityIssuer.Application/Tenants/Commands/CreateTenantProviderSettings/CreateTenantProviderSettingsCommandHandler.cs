using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands.CreateTenantProviderSettings
{
    public class CreateTenantProviderSettingsCommandHandler
        : RequestHandler<CreateTenantProviderSettingsCommand, TenantProviderSettingsDto>
    {
        private readonly ITenantProviderSettingsRepository _providerSettingsRepository;
        private readonly ITenantProvider _tenantProvider;
        private readonly IMapper _mapper;

        public CreateTenantProviderSettingsCommandHandler(
            ITenantProviderSettingsRepository providerSettingsRepository,
            ITenantProvider tenantProvider,
            IMapper mapper)
        {
            _providerSettingsRepository = providerSettingsRepository;
            _tenantProvider = tenantProvider;
            _mapper = mapper;
        }

        public override async Task<RequestResult<TenantProviderSettingsDto>> Handle(
            CreateTenantProviderSettingsCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantProvider.GetTenantAsync(request.TenantCode);
            if (tenant == null)
                return RequestResult<TenantProviderSettingsDto>.Failure(ErrorCode.TenantNotFound,
                    new {request.TenantCode});
            
            if (await _providerSettingsRepository.ProviderSettingsExistsAsync(tenant.Id, request.ProviderType))
                return RequestResult<TenantProviderSettingsDto>
                    .Failure(ErrorCode.TenantProviderSettingsAlreadyExists);

            var createData = _mapper.Map<CreateTenantProviderSettingsData>(request);
            createData.TenantId = tenant.Id;
            var providerSettings = await _providerSettingsRepository
                .CreateTenantProviderSettings(createData);
            
            if(providerSettings == null)
                return RequestResult<TenantProviderSettingsDto>
                    .Failure(ErrorCode.CreateTenantProviderSettingsFailed);
            
            return RequestResult<TenantProviderSettingsDto>
                .Success(_mapper.Map<TenantProviderSettingsDto>(providerSettings));
        }
    }
}