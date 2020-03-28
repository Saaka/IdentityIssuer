using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands
{
    public class RemoveTenantProviderSettingsCommand : Request
    {
        public string TenantCode { get; }
        public AuthProviderType ProviderType { get; }
        public AdminContextData AdminContextData { get; private set; }

        public RemoveTenantProviderSettingsCommand(string tenantCode, AuthProviderType providerType)
        {
            TenantCode = tenantCode;
            ProviderType = providerType;
        }

        public RemoveTenantProviderSettingsCommand WithAdminContextData(AdminContextData value)
        {
            AdminContextData = value;
            return this;
        }
    }
}