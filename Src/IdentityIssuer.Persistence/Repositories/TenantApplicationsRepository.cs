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
    public class TenantApplicationsRepository : ITenantApplicationsRepository
    {
        private readonly AppIdentityContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TenantApplicationsRepository> _logger;

        public TenantApplicationsRepository(
            AppIdentityContext context,
            IMapper mapper,
            ILogger<TenantApplicationsRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<TenantApplication> CreateTenantApplication(SaveTenantApplicationData createApplicationData)
        {
            try
            {
                var entity = _mapper.Map<TenantApplicationEntity>(createApplicationData);
                _context.TenantApplications.Add(entity);
                await _context.SaveChangesAsync();
                
                return _mapper.Map<TenantApplication>(entity);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return null;
            }
        }
    }
}
