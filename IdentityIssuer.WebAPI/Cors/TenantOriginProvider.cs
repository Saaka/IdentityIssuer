using IdentityIssuer.Application.Tenants.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace IdentityIssuer.WebAPI.Cors
{
    public interface ITenantOriginProvider
    {
        Task<string> GetAllowedOrigin(string tenantCode);
    }

    public class TenantOriginProvider : ITenantOriginProvider
    {
        private const string OriginCachePrefix = "_AO_";
        private readonly IMemoryCache cache;
        private readonly ITenantsRepository tenantsRepository;

        public TenantOriginProvider(
            IMemoryCache cache,
            ITenantsRepository tenantsRepository)
        {
            this.cache = cache;
            this.tenantsRepository = tenantsRepository;
        }

        public async Task<string> GetAllowedOrigin(string tenantCode)
        {
            var origin = await cache.GetOrCreateAsync($"{OriginCachePrefix}{tenantCode}", async (ce) =>
            {
                ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                var tenant = await tenantsRepository.GetTenant(tenantCode);
                if (tenant == null)
                    throw new ArgumentException();

                return tenant.AllowedOrigin;
            });

            return origin;
        }
    }
}
