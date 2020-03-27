using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Tenants.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityIssuer.Persistence.Entities
{
    public class TenantAllowedOriginsRepository : ITenantAllowedOriginsRepository
    {
        private readonly AppIdentityContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TenantAllowedOriginsRepository> _logger;

        public TenantAllowedOriginsRepository(
            AppIdentityContext context,
            IMapper mapper,
            ILogger<TenantAllowedOriginsRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> SaveAllowedOriginsAsync(int tenantId, IEnumerable<string> origins)
        {
            try
            {
                var entities = await GetAllowedOriginEntities(tenantId);

                foreach (var entity in entities.Where(entity => !origins.Contains(entity.AllowedOrigin)))
                    _context.Entry(entity).State = EntityState.Deleted;

                foreach (var origin in origins)
                {
                    if (!entities.Select(x => x.AllowedOrigin).Contains(origin))
                        _context.TenantAllowedOrigins
                            .Add(new TenantAllowedOriginEntity {TenantId = tenantId, AllowedOrigin = origin});
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public Task<List<string>> GetAllowedOriginsForTenant(string code)
        {
            var query = from tenant in _context.Tenants
                join ao in _context.TenantAllowedOrigins on tenant.Id equals ao.TenantId
                where tenant.Code == code
                select ao.AllowedOrigin;

            return query.ToListAsync();
        }

        private Task<List<TenantAllowedOriginEntity>> GetAllowedOriginEntities(int tenantId)
        {
            return _context.TenantAllowedOrigins.Where(x => x.TenantId == tenantId).ToListAsync();
        }
    }
}