using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Requests;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Tenants.Commands
{
    public class CreateTenantProviderSettingsCommand : Request<TenantProviderSettingsDto>
    {
        public string TenantCode { get; }
        public AuthProviderType ProviderType { get; }
        public string Identifier { get; }
        public string Key { get; }
        public AdminContextData AdminContextData { get; private set; }
        
        public CreateTenantProviderSettingsCommand(
            string tenantCode, AuthProviderType providerType, 
            string identifier, string key)
        {
            TenantCode = tenantCode;
            ProviderType = providerType;
            Identifier = identifier;
            Key = key;
        }

        public CreateTenantProviderSettingsCommand WithAdminContextData(AdminContextData value)
        {
            AdminContextData = value;
            return this;
        }
    }
}