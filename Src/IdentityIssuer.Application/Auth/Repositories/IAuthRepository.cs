using System.Threading.Tasks;
using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Auth.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> IsEmailRegisteredForTenant(string email, int tenantId);
        Task<TenantUser> GetUserByCredentials(string email, string password, int tenantId);
        Task<TenantUser> GetUserByEmail(string email, int tenantId);
        Task<TenantUser> CreateUser(CreateUserData data);
        Task<TenantUser> CreateGoogleUser(CreateUserData data);
        Task<TenantUser> CreateFacebookUser(CreateUserData data);
        Task<bool> GoogleUserExists(string externalUserId, int tenantId);
        Task<bool> FacebookUserExists(string externalUserId, int tenantId);
        Task<TenantUser> AddGoogleLoginToUser(int tenantId, string email, string externalUserId);
        Task<TenantUser> AddFacebookLoginToUser(int tenantId, string email, string externalUserId);
        Task<TenantUser> UpdateExistingGoogleUser(int tenantId, string email, string imageUrl);
        Task<TenantUser> UpdateExistingFacebookUser(int tenantId, string email, string imageUrl);
    }
}