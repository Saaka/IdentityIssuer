using System;
using System.Threading.Tasks;
using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Users.Repositories
{
    public interface IUserRepository
    {
        Task<TenantUser> GetUser(int userId, int tenantId);
        Task<int> GetUserId(Guid guid);
        Task<TenantUser> UpdateUserDisplayName(Guid userGuid, string name);
    }
}