using System.Threading.Tasks;
using IdentityIssuer.Application.Tenants.Models;

namespace IdentityIssuer.Application.Tenants.Repositories
{
    public interface ITenantApplicationsRepository
    {
        Task CreateTenantApplication(SaveTenantApplicationData createApplicationData);
    }
}