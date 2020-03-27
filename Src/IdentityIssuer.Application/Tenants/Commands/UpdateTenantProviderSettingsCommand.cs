using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands
{
    public class UpdateTenantProviderSettingsCommand : Request<TenantProviderSettingsDto>
    {
        public string TenantCode { get; }
        public AuthProviderType ProviderType { get; }
        public string Identifier { get; }
        public string Key { get; }
        public AdminContextData AdminContextData { get; private set; }

        public UpdateTenantProviderSettingsCommand(
            string tenantCode, AuthProviderType providerType,
            string identifier, string key)
        {
            TenantCode = tenantCode;
            ProviderType = providerType;
            Identifier = identifier;
            Key = key;
        }

        public UpdateTenantProviderSettingsCommand WithAdminContextData(AdminContextData value)
        {
            AdminContextData = value;
            return this;
        }
    }
}