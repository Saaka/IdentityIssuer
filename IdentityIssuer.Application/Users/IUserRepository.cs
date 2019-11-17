using System.Threading.Tasks;
using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Users
{
    public interface IUserRepository
    {
        Task<TenantUser> GetUser(int userId, int tenantId);
    }
}