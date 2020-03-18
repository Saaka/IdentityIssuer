using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants.Models;

namespace IdentityIssuer.Application.Tenants.Repositories
{
    public interface ITenantsRepository
    {
        Task<Tenant> GetTenantAsync(string code);
        Task<IEnumerable<string>> GetAllAllowedOriginsAsync();
        Task<TenantSettings> GetTenantSettingsAsync(string code);
        TenantSettings GetTenantSettings(string code);
        Task<TenantSettings> GetTenantSettings(int tenantId);
        Task<bool> TenantCodeExists(string code);
        Task<Tenant> CreateTenant(CreateTenantDto model);
    }
}
