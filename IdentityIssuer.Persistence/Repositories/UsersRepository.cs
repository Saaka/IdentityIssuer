using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Users.Repositories;
using System.Linq;
using IdentityIssuer.Application.Users.Models;
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

        public async Task<TenantUser> GetUser(string guid)
        {
            var query = from u in context.Users
                where u.UserGuid == guid
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

        public async Task<bool> IsEmailRegisteredForTenant(string email, int tenantId)
        {
            var normalizedEmail = email.ToUpper();
            var query = from u in context.Users
                where u.TenantId == tenantId &&
                      u.NormalizedEmail == normalizedEmail
                select u;

            return await query.AnyAsync();
        }

        public async Task<bool> GoogleUserExists(string externalUserId, int tenantId)
        {
            var query = from u in context.Users
                where u.TenantId == tenantId &&
                      u.GoogleId == externalUserId
                select u.Id;

            return await query.AnyAsync();
        }

        public async Task<TenantUser> CreateUser(CreateUserDto data)
        {
            var tenantUser = new TenantUserEntity
            {
                Email = data.Email,
                UserName = data.UserGuid,
                DisplayName = data.DisplayName,
                UserGuid = data.UserGuid,
                ImageUrl = data.ImageUrl,
                TenantId = data.TenantId
            };

            var result = await userManager.CreateAsync(tenantUser, data.Password);
            if (!result.Succeeded)
                throw new RepositoryException(nameof(CreateUser), result.Errors.Select(x => x.Code));

            return mapper.Map<TenantUser>(tenantUser);
        }

        public async Task<TenantUser> GetUserByCredentials(string email, string password, int tenantId)
        {
            var normalizedEmail = email.ToUpper();
            var query = from u in context.Users
                where u.NormalizedEmail == normalizedEmail
                      && u.TenantId == tenantId
                select u;

            var user = await query.FirstOrDefaultAsync();

            if (await userManager.CheckPasswordAsync(user, password))
                return mapper.Map<TenantUser>(user);

            return null;
        }
    }
}