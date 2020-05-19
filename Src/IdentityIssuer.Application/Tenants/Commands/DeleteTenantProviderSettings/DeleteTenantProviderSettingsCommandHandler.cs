using System;
using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands.DeleteTenantProviderSettings
{
    public class DeleteTenantProviderSettingsCommandHandler
        : RequestHandler<DeleteTenantProviderSettingsCommand, Guid>
    {
        private readonly ITenantProviderSettingsRepository _providerSettingsRepository;
        private readonly ITenantProvider _tenantProvider;

        public DeleteTenantProviderSettingsCommandHandler(
            ITenantProviderSettingsRepository providerSettingsRepository,
            ITenantProvider tenantProvider)
        {
            _providerSettingsRepository = providerSettingsRepository;
            _tenantProvider = tenantProvider;
        }

        public override async Task<RequestResult<Guid>> Handle(
            DeleteTenantProviderSettingsCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantProvider.GetTenantAsync(request.TenantCode);
            if (tenant == null)
                return RequestResult.Failure(ErrorCode.TenantNotFound,
                    new {request.TenantCode});
            
            if (!await _providerSettingsRepository.ProviderSettingsExistsAsync(tenant.Id, request.ProviderType))
                return RequestResult
                    .Failure(ErrorCode.TenantProviderSettingsNotFound);

            if(!await _providerSettingsRepository
                .RemoveTenantProviderSettings(tenant.Id, request.ProviderType))
                return RequestResult
                    .Failure(ErrorCode.DeleteTenantProviderSettingsFailed);

            return RequestResult
                .Success(request.RequestGuid);
        }
    }
}