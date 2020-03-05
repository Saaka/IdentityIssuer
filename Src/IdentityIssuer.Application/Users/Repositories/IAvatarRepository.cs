using System.Threading.Tasks;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Users.Repositories
{
    public interface IAvatarRepository
    {
        Task AddAvatar(int userId, AvatarType type, string imageUrl);
        Task UpdateAvatar(int userId, AvatarType type, string imageUrl);
        Task<TenantUserAvatar> GetAvatar(int userId, AvatarType type);
    }
}