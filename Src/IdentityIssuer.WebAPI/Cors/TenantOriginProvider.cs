using IdentityIssuer.Common.Constants;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants;
using IdentityIssuer.Application.Tenants.Repositories;

namespace IdentityIssuer.WebAPI.Cors
{
    public interface ITenantOriginProvider
    {
        Task<IEnumerable<string>> GetAllowedOrigin(string tenantCode);
    }

    public class TenantOriginProvider : ITenantOriginProvider
    {
        private readonly ICacheStore _cache;
        private readonly ITenantAllowedOriginsRepository _tenantAllowedOriginsRepository;

        public TenantOriginProvider(
            ITenantAllowedOriginsRepository tenantAllowedOriginsRepository,
            ICacheStore cache)
        {
            _cache = cache;
            _tenantAllowedOriginsRepository = tenantAllowedOriginsRepository;
        }

        public async Task<IEnumerable<string>> GetAllowedOrigin(string tenantCode)
        {
            var origins = await _cache.GetOrCreateAsync($"{CacheConstants.OriginCachePrefix}{tenantCode}", async (ce) =>
            {
                ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                var allowedOrigins = await _tenantAllowedOriginsRepository.GetAllowedOriginsForTenant(tenantCode);

                return allowedOrigins;
            });

            return origins;
        }
    }
}
