using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands
{
    public class DeleteTenantProviderSettingsCommand : Request
    {
        public string TenantCode { get; }
        public AuthProviderType ProviderType { get; }

        public DeleteTenantProviderSettingsCommand(string tenantCode, AuthProviderType providerType)
        {
            TenantCode = tenantCode;
            ProviderType = providerType;
        }
    }
}