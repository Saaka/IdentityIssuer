using System.Threading.Tasks;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Tenants.Repositories
{
    public interface ITenantProviderSettingsRepository
    {
        Task<TenantProviderSettings> GetProviderSettings(int tenantId, AuthProviderType providerType);
    }
}