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
        Task<Tenant> GetTenant(string tenantCode);
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

        public async Task<Tenant> GetTenant(string tenantCode)
        {
            var result = await cache.GetOrCreateAsync($"{CacheConstants.TenantCachePrefix}{tenantCode}", async (ce) =>
            {
                ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                var tenant = await tenantsRepository.GetTenant(tenantCode);
                if (tenant == null)
                    throw new TenantNotFoundException(tenantCode);

                return tenant;
            });
            return result;
        }
    }
}
