using System;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Persistence.Entities;
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
            var settingsEntity = _mapper.Map<TenantSettingsEntity>(model);

            try
            {
                _context.TenantSettings.Add(settingsEntity);
                await _context.SaveChangesAsync();

                return _mapper.Map<TenantSettings>(settingsEntity);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return null;
            }
        }
    }
}