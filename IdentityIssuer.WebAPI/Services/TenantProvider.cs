﻿using IdentityIssuer.Application.Tenants;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Common.Constants;
using IdentityIssuer.Common.Exceptions;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace IdentityIssuer.WebAPI.Services
{
    public interface ITenantProvider
    {
        Task<Tenant> GetTenant(string tenantCode);
    }

    public class TenantProvider : ITenantProvider
    {
        private readonly ITenantsRepository tenantsRepository;
        private readonly IMemoryCache cache;

        public TenantProvider(ITenantsRepository tenantsRepository,
            IMemoryCache cache)
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
