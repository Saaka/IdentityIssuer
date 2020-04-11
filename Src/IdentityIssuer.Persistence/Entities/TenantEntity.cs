using System.Collections.Generic;

namespace IdentityIssuer.Persistence.Entities
{
    public class TenantEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual TenantSettingsEntity TenantSettings { get; set; }
        public virtual TenantApplicationEntity TenantApplication { get; set; }

        public virtual ICollection<TenantUserEntity> Users { get; set; }
            = new List<TenantUserEntity>();

        public virtual ICollection<TenantAllowedOriginEntity> AllowedOrigins { get; set; }
            = new List<TenantAllowedOriginEntity>();

        public virtual ICollection<TenantProviderSettingsEntity> TenantProviders { get; set; }
            = new List<TenantProviderSettingsEntity>();
    }
}