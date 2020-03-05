using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Common.Enums;
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
            var query = from tenant in _context.Tenants
                join settings in _context.TenantSettings on tenant.Id equals settings.TenantId
                join providerSettings in _context.TenantProviderSettings
                    on settings.Id equals providerSettings.TenantSettingsId
                where tenant.Id == tenantId && 
                    providerSettings.ProviderType == providerType
                select providerSettings;

            var result = await query.FirstOrDefaultAsync();

            return _mapper.Map<TenantProviderSettings>(result);
        }
    }
}