using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Persistence.Entities
{
    public class TenantProviderSettingsEntity
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public AuthProviderType ProviderType { get; set; }
        public string Identifier { get; set; }
        public string Key { get; set; }

        public virtual TenantEntity Tenant { get; set; }
    }
}