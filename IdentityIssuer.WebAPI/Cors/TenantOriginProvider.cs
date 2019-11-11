using IdentityIssuer.Common.Constants;
using System;
using System.Threading.Tasks;
using IdentityIssuer.Application.Tenants;
using IdentityIssuer.Common.Services;

namespace IdentityIssuer.WebAPI.Cors
{
    public interface ITenantOriginProvider
    {
        Task<string> GetAllowedOrigin(string tenantCode);
    }

    public class TenantOriginProvider : ITenantOriginProvider
    {
        private readonly ICacheStore cache;
        private readonly ITenantProvider tenantProvider;

        public TenantOriginProvider(
            ITenantProvider tenantProvider,
            ICacheStore cache)
        {
            this.cache = cache;
            this.tenantProvider = tenantProvider;
        }

        public async Task<string> GetAllowedOrigin(string tenantCode)
        {
            var origin = await cache.GetOrCreateAsync($"{CacheConstants.OriginCachePrefix}{tenantCode}", async (ce) =>
            {
                ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                var tenant = await tenantProvider.GetTenantAsync(tenantCode);

                return tenant?.AllowedOrigin;
            });

            return origin;
        }
    }
}
