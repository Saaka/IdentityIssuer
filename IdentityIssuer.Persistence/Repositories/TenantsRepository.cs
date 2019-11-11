using IdentityIssuer.Application.Tenants.Repositories;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using IdentityIssuer.Application.Models;

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

        public async Task<IEnumerable<string>> GetAllAllowedOriginsAsync()
        {
            var query = from tenant in context.Tenants
                select tenant.AllowedOrigin;

            return await query.ToListAsync();
        }

        public async Task<Tenant> GetTenantAsync(string code)
        {
            var query = from tenant in context.Tenants
                where tenant.Code == code
                select tenant;

            var result = await query.FirstOrDefaultAsync();

            return mapper.Map<Tenant>(result);
        }

        public async Task<TenantSettings> GetTenantSettingsAsync(string code)
        {
            var query = from ts in context.TenantSettings
                join t in context.Tenants on ts.TenantId equals t.Id
                where t.Code == code
                select ts;

            var result = await query.FirstOrDefaultAsync();

            return mapper.Map<TenantSettings>(result);
        }

        public TenantSettings GetTenantSettings(string code)
        {
            var query = from ts in context.TenantSettings
                join t in context.Tenants on ts.TenantId equals t.Id
                where t.Code == code
                select ts;

            var result = query.FirstOrDefault();

            return mapper.Map<TenantSettings>(result);
        }
    }
}