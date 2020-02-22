using System.Threading.Tasks;
using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Users.Repositories
{
    public interface IUserRepository
    {
        Task<TenantUser> GetUser(int userId, int tenantId);
        Task<int> GetUserId(string guid);
        Task<TenantUser> UpdateUserDisplayName(string userGuid, string name);
    }
}