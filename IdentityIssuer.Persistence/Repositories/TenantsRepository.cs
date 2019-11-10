using IdentityIssuer.Application.Tenants;
using IdentityIssuer.Application.Tenants.Repositories;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace IdentityIssuer.Persistence.Repositories
{
    public class TenantsRepository : ITenantsRepository
    {
        private readonly AppIdentityContext context;
        private readonly IMapper mapper;

        public TenantsRepository(AppIdentityContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<string>> GetAllAllowedOrigins()
        {
            var query = from tenant in context.Tenants
                select tenant.AllowedOrigin;

            return await query.ToListAsync();
        }

        public async Task<Tenant> GetTenant(string code)
        {
            var query = from tenant in context.Tenants
                where tenant.Code == code
                select tenant;

            var result = await query.FirstOrDefaultAsync();

            return mapper.Map<Tenant>(result);
        }

        public async Task<TenantSettings> GetTenantSettings(int tenantId)
        {
            var query = from tc in context.TenantSettings
                where tc.TenantId == tenantId
                select tc;

            var result = await query.FirstOrDefaultAsync();

            return mapper.Map<TenantSettings>(result);
        }
    }
}