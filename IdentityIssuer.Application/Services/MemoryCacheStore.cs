using System;
using System.Threading.Tasks;
using IdentityIssuer.Common.Services;
using Microsoft.Extensions.Caching.Memory;

namespace IdentityIssuer.Application.Services
{
    public class MemoryCacheStore : ICacheStore
    {
        private readonly IMemoryCache memoryCache;

        public MemoryCacheStore(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public async Task<TItem> GetOrCreateAsync<TItem>(object key, Func<ICacheEntry, Task<TItem>> factory)
        {
            return await memoryCache.GetOrCreateAsync(key, factory);
        }

        public TItem GetOrCreate<TItem>(object key, Func<ICacheEntry, TItem> factory)
        {
            return memoryCache.GetOrCreate(key, factory);
        }
    }
}