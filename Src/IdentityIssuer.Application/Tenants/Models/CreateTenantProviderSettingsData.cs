using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Tenants.Models
{
    public class CreateTenantProviderSettingsData
    {
        public int TenantId { get; set; }
        public AuthProviderType ProviderType { get; set; }
        public string Identifier { get; set; }
        public string Key { get; set; }
    }
}