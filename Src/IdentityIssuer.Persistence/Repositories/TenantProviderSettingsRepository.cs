using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityIssuer.Persistence.Repositories
{
    public class TenantProviderSettingsRepository : ITenantProviderSettingsRepository
    {
        private readonly AppIdentityContext _context;
        private readonly IMapper _mapper;

        public TenantProviderSettingsRepository(AppIdentityContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TenantProviderSettings> GetProviderSettings(int tenantId, AuthProviderType providerType)
        {
            var query = GetTenantProviderSettingsQuery(tenantId, providerType);

            var result = await query.FirstOrDefaultAsync();

            return _mapper.Map<TenantProviderSettings>(result);
        }

        public Task<bool> TenantProviderSettingsExists(int tenantId, AuthProviderType providerType)
        {
            var query = GetTenantProviderSettingsQuery(tenantId, providerType);

            return query.Select(x => x.Id).AnyAsync();
        }

        public async Task<TenantProviderSettings> CreateTenantProviderSettings(CreateTenantProviderSettingsDto data)
        {
            var providerSettings = _mapper.Map<TenantProviderSettingsEntity>(data);

            _context.TenantProviderSettings.Add(providerSettings);
            await _context.SaveChangesAsync();

            return _mapper.Map<TenantProviderSettings>(providerSettings);
        }

        private IQueryable<TenantProviderSettingsEntity> GetTenantProviderSettingsQuery(int tenantId,
            AuthProviderType providerType)
        {
            var query = from providerSettings in _context.TenantProviderSettings
                where providerSettings.TenantId == tenantId &&
                      providerSettings.ProviderType == providerType
                select providerSettings;
            return query;
        }
    }
}