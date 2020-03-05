using IdentityIssuer.Application.Tenants.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityIssuer.Application.Services;

namespace IdentityIssuer.WebAPI.Cors
{
    public interface IAllowedOriginsProvider
    {
        Task<bool> IsOriginAvailable(string origin);
    }

    public class AllowedOriginsProvider : IAllowedOriginsProvider
    {
        private const string AllOriginsCachePrefix = "_AAO_";
        private readonly ICacheStore _cache;
        private readonly ITenantsRepository _tenantsRepository;

        public AllowedOriginsProvider(
            ICacheStore cache,
            ITenantsRepository tenantsRepository)
        {
            _cache = cache;
            _tenantsRepository = tenantsRepository;
        }

        public async Task<bool> IsOriginAvailable(string origin)
        {
            var allowedOrigins = await _cache.GetOrCreateAsync(AllOriginsCachePrefix, async (ce) =>
            {
                ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                var origins = await _tenantsRepository.GetAllAllowedOriginsAsync();

                return origins;
            });

            return allowedOrigins.Contains(origin);
        }
    }
}
