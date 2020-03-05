using System;
using System.Threading.Tasks;
using IdentityIssuer.Application.Services;
using Microsoft.Extensions.Caching.Memory;

namespace IdentityIssuer.Infrastructure.Cache
{
    public class MemoryCacheStore : ICacheStore
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheStore(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<TItem> GetOrCreateAsync<TItem>(object key, Func<ICacheEntry, Task<TItem>> factory)
        {
            return await _memoryCache.GetOrCreateAsync(key, factory);
        }

        public TItem GetOrCreate<TItem>(object key, Func<ICacheEntry, TItem> factory)
        {
            return _memoryCache.GetOrCreate(key, factory);
        }
    }
}