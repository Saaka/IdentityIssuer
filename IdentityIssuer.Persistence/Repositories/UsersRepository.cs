using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Users.Repositories;
using System.Linq;
using System.Collections.Generic;
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

        public async Task<TenantUser> GetUser(string guid)
        {
            var query = from u in context.Users
                where  u.UserGuid == guid
                select u;

            var result = await query.FirstOrDefaultAsync();

            return mapper.Map<TenantUser>(result);
        }
    }
}