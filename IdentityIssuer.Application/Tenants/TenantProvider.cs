using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Common.Constants;
using IdentityIssuer.Common.Exceptions;
using System;
using System.Threading.Tasks;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Services;

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
        private readonly ITenantsRepository tenantsRepository;
        private readonly ICacheStore cache;

        public TenantProvider(ITenantsRepository tenantsRepository,
            ICacheStore cache)
        {
            this.tenantsRepository = tenantsRepository;
            this.cache = cache;
        }

        public async Task<Tenant> GetTenantAsync(string tenantCode)
        {
            var result = await cache.GetOrCreateAsync($"{CacheConstants.TenantCachePrefix}{tenantCode}",
                async (ce) =>
                {
                    ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                    ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                    var tenant = await tenantsRepository.GetTenantAsync(tenantCode);
                    if (tenant == null)
                        throw new TenantNotFoundException(tenantCode);

                    return tenant;
                });
            return result;
        }

        public async Task<TenantSettings> GetTenantSettingsAsync(string tenantCode)
        {
            var result = await cache.GetOrCreateAsync($"{CacheConstants.TenantSettingsCachePrefix}{tenantCode}",
                async (ce) => { 
                    ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                    ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                    var settings = await tenantsRepository.GetTenantSettingsAsync(tenantCode);

                    return settings;
                });

            return result;
        }

        public TenantSettings GetTenantSettings(string tenantCode)
        {
            var result = cache.GetOrCreate($"{CacheConstants.TenantSettingsCachePrefix}{tenantCode}",
                (ce) => {
                    ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                    ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                    var settings = tenantsRepository.GetTenantSettings(tenantCode);

                    return settings;
                });

            return result;
        }
    }
}