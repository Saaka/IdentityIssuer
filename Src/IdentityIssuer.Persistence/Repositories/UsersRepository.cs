using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Users.Repositories;
using System.Linq;
using IdentityIssuer.Common.Exceptions;
using IdentityIssuer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityIssuer.Persistence.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly AppIdentityContext context;
        private readonly IMapper mapper;

        public UsersRepository(AppIdentityContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TenantUser> GetUser(int userId, int tenantId)
        {
            var query = from u in context.Users
                where u.Id == userId && u.TenantId == tenantId
                select u;

            var result = await query.FirstOrDefaultAsync();

            return mapper.Map<TenantUser>(result);
        }

        public async Task<int> GetUserId(string guid)
        {
            var query = from u in context.Users
                where u.UserGuid == guid
                select u.Id;

            var result = await query.FirstOrDefaultAsync();

            return result;
        }

        public async Task<TenantUser> UpdateUserDisplayName(string userGuid, string name)
        {
            var user = await GetUser(userGuid);

            user.DisplayName = name;
            await  context.SaveChangesAsync();

            return mapper.Map<TenantUser>(user);
        }

        private async Task<TenantUserEntity> GetUser(string guid)
        {
            var query = from u in context.Users
                where u.UserGuid == guid
                select u;

            var user = await query.FirstOrDefaultAsync();
            if (user == null)
                throw new UserNotFoundException(guid);

            return user;
        }
    }
}