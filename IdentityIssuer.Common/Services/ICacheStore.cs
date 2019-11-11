using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace IdentityIssuer.Common.Services
{
    public interface ICacheStore
    {
        Task<TItem> GetOrCreateAsync<TItem>(object key, Func<ICacheEntry, Task<TItem>> factory);
    }
}