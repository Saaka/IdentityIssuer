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

        public async Task<TenantUser> GetUser(int userId, int tenantId)
        {
            var query = from u in _context.Users
                where u.Id == userId && u.TenantId == tenantId
                select u;

            var result = await query.FirstOrDefaultAsync();

            return _mapper.Map<TenantUser>(result);
        }

        public async Task<int> GetUserId(Guid guid)
        {
            var query = from u in _context.Users
                where u.UserGuid == guid
                select u.Id;

            var result = await query.FirstOrDefaultAsync();

            return result;
        }

        public async Task<TenantUser> UpdateUserDisplayName(Guid userGuid, string name)
        {
            var user = await GetUser(userGuid);

            user.DisplayName = name;
            await  _context.SaveChangesAsync();

            return _mapper.Map<TenantUser>(user);
        }

        private async Task<TenantUserEntity> GetUser(Guid guid)
        {
            var query = from u in _context.Users
                where u.UserGuid == guid
                select u;

            var user = await query.FirstOrDefaultAsync();
            if (user == null)
                throw new DomainException(ErrorCode.UserNotFound, 
                    new { userGuid = guid });

            return user;
        }
    }
}