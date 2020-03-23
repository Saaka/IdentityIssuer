using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityIssuer.Application.Tenants.Repositories
{
    public interface ITenantAllowedOriginsRepository
    {
        Task<bool> SaveAllowedOrigins(int tenantId, ICollection<string> allowedOrigins);
    }
}