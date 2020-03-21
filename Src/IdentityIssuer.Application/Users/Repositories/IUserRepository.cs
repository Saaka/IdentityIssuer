using System;
using System.Threading.Tasks;
using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Users.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(Guid guid);
        Task<TenantUser> GetUser(int userId, int tenantId);
        Task<TenantUser> GetUser(Guid guid);
        Task<int> GetUserId(Guid guid);
        Task<bool> UpdateUserDisplayName(Guid userGuid, string name);
        Task<bool> SetUserAdminValue(Guid userGuid, bool isAdmin);
    }
}