using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Users.Repositories;
using System.Linq;
using IdentityIssuer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Logging;

namespace IdentityIssuer.Persistence.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly AppIdentityContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersRepository> _logger;

        public UsersRepository(AppIdentityContext context,
            IMapper mapper,
            ILogger<UsersRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
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
            try
            {
                var user = await GetUserEntity(userGuid);

                user.DisplayName = name;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, e.Message);
                return false;
            }
        }

        public async Task<bool> SetUserAdminValue(Guid userGuid, bool isAdmin)
        {
            try
            {
                var user = await GetUserEntity(userGuid);
                user.IsAdmin = isAdmin;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, e.Message);
                return false;
            }
        }

        public async Task<bool> SetUserOwnerValue(Guid userGuid, bool isOwner)
        {
            try
            {
                var user = await GetUserEntity(userGuid);
                user.IsOwner = isOwner;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, e.Message);
                return false;
            }
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