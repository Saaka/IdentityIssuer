using IdentityIssuer.Application.Tenants.Repositories;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Persistence.Entities;

namespace IdentityIssuer.Persistence.Repositories
{
    public class TenantsRepository : ITenantsRepository
    {
        private readonly AppIdentityContext _context;
        private readonly IMapper _mapper;

        public TenantsRepository(AppIdentityContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<string>> GetAllAllowedOriginsAsync()
        {
            var query = from tenant in _context.Tenants
                select tenant.AllowedOrigin;

            return await query.ToListAsync();
        }

        public async Task<Tenant> GetTenantAsync(string code)
        {
            var query = from tenant in _context.Tenants
                where tenant.Code == code
                select tenant;

            var result = await query.FirstOrDefaultAsync();

            return _mapper.Map<Tenant>(result);
        }

        public async Task<TenantSettings> GetTenantSettingsAsync(string code)
        {
            var query = from ts in _context.TenantSettings
                join t in _context.Tenants on ts.TenantId equals t.Id
                where t.Code == code
                select ts;

            var result = await query.FirstOrDefaultAsync();

            return _mapper.Map<TenantSettings>(result);
        }

        public TenantSettings GetTenantSettings(string code)
        {
            var query = from ts in _context.TenantSettings
                join t in _context.Tenants on ts.TenantId equals t.Id
                where t.Code == code
                select ts;

            var result = query.FirstOrDefault();

            return _mapper.Map<TenantSettings>(result);
        }

        public async Task<TenantSettings> GetTenantSettings(int tenantId)
        {
            var query = from ts in _context.TenantSettings
                where ts.TenantId == tenantId
                select ts;

            var result = await query.FirstOrDefaultAsync();

            return _mapper.Map<TenantSettings>(result);
        }

        public Task<bool> TenantCodeExists(string code)
        {
            return _context.Tenants.AnyAsync(x => x.Code == code);
        }

        public async Task<Tenant> CreateTenant(CreateTenantDto model)
        {
            var tenantEntity = new TenantEntity
            {
                Code = model.Code,
                Name = model.Name,
                AllowedOrigin = model.AllowedOrigin
            };
            _context.Tenants.Add(tenantEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<Tenant>(tenantEntity);
        }
    }
}