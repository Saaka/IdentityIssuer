using IdentityIssuer.Application.Tenants.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityIssuer.Common.Services;

namespace IdentityIssuer.WebAPI.Cors
{
    public interface IAllowedOriginsProvider
    {
        Task<bool> IsOriginAvailable(string origin);
    }

    public class AllowedOriginsProvider : IAllowedOriginsProvider
    {
        private const string AllOriginsCachePrefix = "_AAO_";
        private readonly ICacheStore cache;
        private readonly ITenantsRepository tenantsRepository;

        public AllowedOriginsProvider(
            ICacheStore cache,
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

                var origins = await tenantsRepository.GetAllAllowedOriginsAsync();

                return origins;
            });

            return allowedOrigins.Contains(origin);
        }
    }
}
