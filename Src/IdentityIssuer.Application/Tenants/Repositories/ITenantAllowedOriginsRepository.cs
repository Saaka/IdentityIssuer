using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityIssuer.Application.Tenants.Repositories
{
    public interface ITenantAllowedOriginsRepository
    {
        Task<bool> SaveAllowedOriginsAsync(int tenantId, ICollection<string> allowedOrigins);
        Task<List<string>> GetAllowedOriginsForTenant(string code);
    }
}