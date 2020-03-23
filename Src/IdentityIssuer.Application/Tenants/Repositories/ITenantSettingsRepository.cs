using System.Threading.Tasks;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants.Models;

namespace IdentityIssuer.Application.Tenants.Repositories
{
    public interface ITenantSettingsRepository
    {
        Task<TenantSettings> CreateTenantSettings(CreateTenantSettingsDto model);
    }
}