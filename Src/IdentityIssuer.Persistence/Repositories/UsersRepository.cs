using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Users.Repositories;
using System.Linq;
using IdentityIssuer.Common.Exceptions;
using IdentityIssuer.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityIssuer.Persistence.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly AppIdentityContext context;
        private readonly IMapper mapper;
        private readonly UserManager<TenantUserEntity> userManager;

        public UsersRepository(AppIdentityContext context,
            IMapper mapper,
            UserManager<TenantUserEntity> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
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

            return await UpdateUser(user, nameof(UpdateUserDisplayName));
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

        private async Task<TenantUser> UpdateUser(TenantUserEntity user, string method)
        {
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
                return mapper.Map<TenantUser>(user);
            else
                throw new RepositoryException(method, result.Errors.Select(x => x.Code));
        }
    }
}