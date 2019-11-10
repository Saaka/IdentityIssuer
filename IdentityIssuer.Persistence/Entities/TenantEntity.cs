using System.Collections.Generic;

namespace IdentityIssuer.Persistence.Entities
{
    public class TenantEntity
    {
        public TenantEntity()
        {
            Users = new List<TenantUserEntity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string AllowedOrigin { get; set; }

        public virtual TenantSettingsEntity TenantSettings { get; set; }
        public virtual ICollection<TenantUserEntity> Users { get; set; }
    }
}
