using System.Threading.Tasks;
using IdentityIssuer.Application.Tenants.Models;

namespace IdentityIssuer.Application.Services
{
    public interface ITenantInitializer
    {
        Task<TenantDto> InitializeTenantFromConfigurationAsync();
    }
}