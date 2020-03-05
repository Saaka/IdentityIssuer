using System.Threading.Tasks;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Users.Repositories
{
    public interface IAvatarRepository
    {
        Task AddAvatar(int userId, AvatarType type, string imageUrl);
        Task UpdateAvatar(int userId, AvatarType type, string imageUrl);
    }
}