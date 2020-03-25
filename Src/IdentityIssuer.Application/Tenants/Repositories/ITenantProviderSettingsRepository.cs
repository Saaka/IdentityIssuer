using System.Threading.Tasks;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Tenants.Repositories
{
    public interface ITenantProviderSettingsRepository
    {
        Task<TenantProviderSettings> GetProviderSettings(int tenantId, AuthProviderType providerType);
        Task<bool> ProviderSettingsExistsAsync(int tenantId, AuthProviderType providerType);
        Task<TenantProviderSettings> CreateTenantProviderSettings(CreateTenantProviderSettingsDto data);
    }
}