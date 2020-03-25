using System;
using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands.CreateTenantProviderSettings
{
    public class CreateTenantProviderSettingsCommandHandler 
        : RequestHandler<CreateTenantProviderSettingsCommand, TenantProviderSettingsDto>
    {
        private readonly ITenantProviderSettingsRepository _tenantProviderSettingsRepository;

        public CreateTenantProviderSettingsCommandHandler(
            ITenantProviderSettingsRepository tenantProviderSettingsRepository)
        {
            _tenantProviderSettingsRepository = tenantProviderSettingsRepository;
        }
        
        public override async Task<RequestResult<TenantProviderSettingsDto>> Handle(
            CreateTenantProviderSettingsCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}