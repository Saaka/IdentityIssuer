using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Users.Repositories;
using System.Linq;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;
using IdentityIssuer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace IdentityIssuer.Persistence.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly AppIdentityContext _context;
        private readonly IMapper _mapper;

        public UsersRepository(AppIdentityContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<bool> UserExistsAsync(Guid guid)
        {
            return _context.Users.AnyAsync(x => x.UserGuid == guid);
        }

        public async Task<TenantUser> GetUser(int userId, int tenantId)
        {
            var query = from u in _context.Users
                where u.Id == userId && u.TenantId == tenantId
                select u;

            var result = await query.FirstOrDefaultAsync();

            return _mapper.Map<TenantUser>(result);
        }

        public async Task<TenantUser> GetUser(Guid guid)
        {
            var user = await GetUserEntity(guid);

            return _mapper.Map<TenantUser>(user);
        }

        public async Task<int> GetUserId(Guid guid)
        {
            var query = from u in _context.Users
                where u.UserGuid == guid
                select u.Id;

            var result = await query.FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> UpdateUserDisplayName(Guid userGuid, string name)
        {
            var user = await GetUserEntity(userGuid);

            user.DisplayName = name;
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> SetUserAdminValue(Guid userGuid, bool isAdmin)
        {
            var user = await GetUserEntity(userGuid);
            user.IsAdmin = isAdmin;

            return (await _context.SaveChangesAsync()) > 0;
        }

        private async Task<TenantUserEntity> GetUserEntity(Guid guid)
        {
            var query = from u in _context.Users
                where u.UserGuid == guid
                select u;

            var user = await query.FirstOrDefaultAsync();

            return user;
        }
    }
}