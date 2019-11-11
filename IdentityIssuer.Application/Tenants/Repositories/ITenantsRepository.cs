using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Tenants.Repositories
{
    public interface ITenantsRepository
    {
        Task<Tenant> GetTenantAsync(string code);
        Task<IEnumerable<string>> GetAllAllowedOriginsAsync();
        Task<TenantSettings> GetTenantSettingsAsync(string code);
        TenantSettings GetTenantSettings(string code);
    }
}
