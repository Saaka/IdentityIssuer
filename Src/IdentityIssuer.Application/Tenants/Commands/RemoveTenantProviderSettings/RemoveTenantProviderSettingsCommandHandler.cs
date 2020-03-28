using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands.RemoveTenantProviderSettings
{
    public class RemoveTenantProviderSettingsCommandHandler
        : RequestHandler<RemoveTenantProviderSettingsCommand, Guid>
    {
        private readonly ITenantProviderSettingsRepository _providerSettingsRepository;
        private readonly ITenantProvider _tenantProvider;
        private readonly IMapper _mapper;

        public RemoveTenantProviderSettingsCommandHandler(
            ITenantProviderSettingsRepository providerSettingsRepository,
            ITenantProvider tenantProvider,
            IMapper mapper)
        {
            _providerSettingsRepository = providerSettingsRepository;
            _tenantProvider = tenantProvider;
            _mapper = mapper;
        }

        public override async Task<RequestResult<Guid>> Handle(
            RemoveTenantProviderSettingsCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantProvider.GetTenantAsync(request.TenantCode);
            if (tenant == null)
                return RequestResult.Failure(ErrorCode.TenantNotFound,
                    new {Code = request.TenantCode});
            
            if (!await _providerSettingsRepository.ProviderSettingsExistsAsync(tenant.Id, request.ProviderType))
                return RequestResult
                    .Failure(ErrorCode.TenantProviderSettingsNotFound);

            if(!await _providerSettingsRepository
                .RemoveTenantProviderSettings(tenant.Id, request.ProviderType))
                return RequestResult
                    .Failure(ErrorCode.RemoveTenantProviderSettingsFailed);

            return RequestResult
                .Success(request.RequestGuid);
        }
    }
}