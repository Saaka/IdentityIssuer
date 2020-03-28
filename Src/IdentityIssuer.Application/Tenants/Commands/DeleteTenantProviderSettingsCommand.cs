using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands
{
    public class DeleteTenantProviderSettingsCommand : Request
    {
        public string TenantCode { get; }
        public AuthProviderType ProviderType { get; }
        public AdminContextData AdminContextData { get; private set; }

        public DeleteTenantProviderSettingsCommand(string tenantCode, AuthProviderType providerType)
        {
            TenantCode = tenantCode;
            ProviderType = providerType;
        }

        public DeleteTenantProviderSettingsCommand WithAdminContextData(AdminContextData value)
        {
            AdminContextData = value;
            return this;
        }
    }
}