using System.Threading.Tasks;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Users.Models;

namespace IdentityIssuer.Application.Users.Repositories
{
    public interface IUserRepository
    {
        Task<TenantUser> GetUser(int userId, int tenantId);
        Task<TenantUser> GetUser(string guid);
        Task<bool> IsEmailUniqueForTenant(string email, int tenantId);
        Task<TenantUser> CreateUser(CreateUserDto data);
    }
}