using IdentityIssuer.Application.Tenants.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityIssuer.WebAPI.Cors
{
    public interface IAllowedOriginsProvider
    {
        Task<bool> IsOriginAvailable(string origin);
    }

    public class AllowedOriginsProvider : IAllowedOriginsProvider
    {
        private const string AllOriginsCachePrefix = "_AAO_";
        private readonly IMemoryCache cache;
        private readonly ITenantsRepository tenantsRepository;

        public AllowedOriginsProvider(
            IMemoryCache cache,
            ITenantsRepository tenantsRepository)
        {
            this.cache = cache;
            this.tenantsRepository = tenantsRepository;
        }

        public async Task<bool> IsOriginAvailable(string origin)
        {
            var allowedOrigins = await cache.GetOrCreateAsync(AllOriginsCachePrefix, async (ce) =>
            {
                ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                var origins = await tenantsRepository.GetAllAllowedOrigins();

                return origins;
            });

            return allowedOrigins.Contains(origin);
        }
    }
}
