using IdentityIssuer.Common.Constants;
using System;
using System.Threading.Tasks;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants;

namespace IdentityIssuer.WebAPI.Cors
{
    public interface ITenantOriginProvider
    {
        Task<string> GetAllowedOrigin(string tenantCode);
    }

    public class TenantOriginProvider : ITenantOriginProvider
    {
        private readonly ICacheStore _cache;
        private readonly ITenantProvider _tenantProvider;

        public TenantOriginProvider(
            ITenantProvider tenantProvider,
            ICacheStore cache)
        {
            _cache = cache;
            _tenantProvider = tenantProvider;
        }

        public async Task<string> GetAllowedOrigin(string tenantCode)
        {
            var origin = await _cache.GetOrCreateAsync($"{CacheConstants.OriginCachePrefix}{tenantCode}", async (ce) =>
            {
                ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                var tenant = await _tenantProvider.GetTenantAsync(tenantCode);

                return tenant?.AllowedOrigin;
            });

            return origin;
        }
    }
}
