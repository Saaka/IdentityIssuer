using System.Linq;
using System.Threading.Tasks;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Users.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityIssuer.Persistence.Repositories
{
    public class AvatarRepository : IAvatarRepository
    {
        private readonly AppIdentityContext _context;

        public AvatarRepository(AppIdentityContext context)
        {
            _context = context;
        }

        public async Task StoreAvatar(int userId, AvatarType type, string imageUrl)
        {
            var avatar = await GetAvatarEntity(userId, type);
            if (avatar == null)
            {
                await AddAvatar(userId, type, imageUrl);
                return;
            }

            avatar.Url = imageUrl;
            await _context.SaveChangesAsync();
        }

        private async Task AddAvatar(int userId, AvatarType type, string imageUrl)
        {
            _context.UserAvatars.Add(new TenantUserAvatarEntity
            {
                TenantUserId = userId,
                AvatarType = type,
                Url = imageUrl,
            });
            await _context.SaveChangesAsync();
        }

        public async Task<TenantUserAvatar> GetAvatar(int userId, AvatarType type)
        {
            var entity = await GetAvatarEntity(userId, type);
            return new TenantUserAvatar
            {
                Id = entity.Id,
                AvatarType = entity.AvatarType,
                ImageUrl = entity.Url,
                TenantUserId = entity.TenantUserId
            };
        }

        private async Task<TenantUserAvatarEntity> GetAvatarEntity(int userId, AvatarType type)
        {
            return await _context
                .UserAvatars
                .Where(x => x.TenantUserId == userId && x.AvatarType == type)
                .FirstOrDefaultAsync();
        }
    }
}