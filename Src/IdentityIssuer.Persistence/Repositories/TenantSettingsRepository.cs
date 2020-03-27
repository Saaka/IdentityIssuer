using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityIssuer.Persistence.Repositories
{
    public class TenantSettingsRepository : ITenantSettingsRepository
    {
        private readonly AppIdentityContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TenantSettingsRepository> _logger;

        public TenantSettingsRepository(AppIdentityContext context,
            IMapper mapper,
            ILogger<TenantSettingsRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TenantSettings> SaveTenantSettings(SaveTenantSettingsData model)
        {
            try
            {
                var tenantSettings = await GetTenantSettings(model.TenantId);
                if (tenantSettings == null)
                    return await CreateTenantSettings(model);

                return await UpdateTenantSettings(tenantSettings, model);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return null;
            }
        }

        private async Task<TenantSettings> UpdateTenantSettings(TenantSettingsEntity entity, SaveTenantSettingsData model)
        {
            _mapper.Map(model, entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<TenantSettings>(entity);
        }

        private async Task<TenantSettings> CreateTenantSettings(SaveTenantSettingsData model)
        {
            var settingsEntity = _mapper.Map<TenantSettingsEntity>(model);
            _context.TenantSettings.Add(settingsEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TenantSettings>(settingsEntity);
        }

        private Task<TenantSettingsEntity> GetTenantSettings(int tenantId)
        {
            var query = from ts in _context.TenantSettings
                where ts.TenantId == tenantId
                select ts;

            return query.FirstOrDefaultAsync();
        }
    }
}