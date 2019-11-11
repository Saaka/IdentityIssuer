using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Tenants.Repositories
{
    public interface ITenantsRepository
    {
        Task<Tenant> GetTenant(string code);
        Task<IEnumerable<string>> GetAllAllowedOrigins();
        Task<TenantSettings> GetTenantSettings(int tenantId);
    }
}
