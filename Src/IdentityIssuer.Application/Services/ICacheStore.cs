using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace IdentityIssuer.Application.Services
{
    public interface ICacheStore
    {
        Task<TItem> GetOrCreateAsync<TItem>(object key, Func<ICacheEntry, Task<TItem>> factory);
        TItem GetOrCreate<TItem>(object key, Func<ICacheEntry, TItem> factory);
    }
}