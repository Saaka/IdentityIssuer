using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Tenants
{
    public class TenantProvider
    {
        public int Id { get; set; }
        public AuthProviderType ProviderType { get; set; }
        public string Identifier { get; set; }
        public string Key { get; set; }
    }
}