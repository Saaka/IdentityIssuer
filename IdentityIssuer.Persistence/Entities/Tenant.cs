using System.Collections.Generic;

namespace IdentityIssuer.Persistence.Entities
{
    public class Tenant
    {
        public Tenant()
        {
            Users = new List<TenantUser>();
        }

        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string AllowedOrigin { get; set; }

        public virtual ICollection<TenantUser> Users { get; set; }
    }
}
