using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Persistence.Entities
{
    public class TenantProviderEntity
    {
        public int Id { get; set; }
        public int TenantConfigurationId { get; set; }
        public AuthProviderType ProviderType { get; set; }
        public string Identifier { get; set; }
        public string Key { get; set; }

        public virtual TenantConfigurationEntity TenantConfiguration { get; set; }
    }
}