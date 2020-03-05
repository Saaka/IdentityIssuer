using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Common.Constants;
using IdentityIssuer.Common.Exceptions;
using System;
using System.Threading.Tasks;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Tenants
{
    public interface ITenantProvider
    {
        Task<Tenant> GetTenantAsync(string tenantCode);
        Task<TenantSettings> GetTenantSettingsAsync(string tenantCode);
        TenantSettings GetTenantSettings(string tenantCode);
    }

    public class TenantProvider : ITenantProvider
    {
        private readonly ITenantsRepository _tenantsRepository;
        private readonly ICacheStore _cache;

        public TenantProvider(ITenantsRepository tenantsRepository,
            ICacheStore cache)
        {
            _tenantsRepository = tenantsRepository;
            _cache = cache;
        }

        public async Task<Tenant> GetTenantAsync(string tenantCode)
        {
            var result = await _cache.GetOrCreateAsync($"{CacheConstants.TenantCachePrefix}{tenantCode}",
                async (ce) =>
                {
                    ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                    ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                    var tenant = await _tenantsRepository.GetTenantAsync(tenantCode);
                    if (tenant == null)
                        throw new DomainException(ExceptionCode.TenantNotFound, new { tenantCode });

                    return tenant;
                });
            return result;
        }

        public async Task<TenantSettings> GetTenantSettingsAsync(string tenantCode)
        {
            var result = await _cache.GetOrCreateAsync($"{CacheConstants.TenantSettingsCachePrefix}{tenantCode}",
                async (ce) => { 
                    ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                    ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                    var settings = await _tenantsRepository.GetTenantSettingsAsync(tenantCode);

                    return settings;
                });

            return result;
        }

        public TenantSettings GetTenantSettings(string tenantCode)
        {
            var result = _cache.GetOrCreate($"{CacheConstants.TenantSettingsCachePrefix}{tenantCode}",
                (ce) => {
                    ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                    ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                    var settings = _tenantsRepository.GetTenantSettings(tenantCode);

                    return settings;
                });

            return result;
        }
    }
}