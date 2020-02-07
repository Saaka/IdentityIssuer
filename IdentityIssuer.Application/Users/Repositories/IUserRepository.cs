using System.Threading.Tasks;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Users.Models;

namespace IdentityIssuer.Application.Users.Repositories
{
    public interface IUserRepository
    {
        Task<TenantUser> GetUser(int userId, int tenantId);
        Task<TenantUser> GetUser(string guid);
        Task<int> GetUserId(string guid);
        Task<bool> IsEmailRegisteredForTenant(string email, int tenantId);
        Task<TenantUser> CreateUser(CreateUserDto data);
        Task<TenantUser> CreateGoogleUser(CreateUserDto data);
        Task<TenantUser> GetUserByCredentials(string email, string password, int tenantId);
        Task<bool> GoogleUserExists(string externalUserId, int tenantId);
        Task<TenantUser> AddGoogleLoginToUser(int tenantId, string email, string externalUserId, string imageUrl);
        Task<TenantUser> UpdateExistingGoogleUser(int tenantId, string email, string imageUrl);
    }
}