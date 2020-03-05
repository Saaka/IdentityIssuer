using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityIssuer.UnitTests.Utils.Fixtures
{
    /// <summary>
    /// Helper class that creates fully working IMemoryCache 
    /// </summary>
    public class MemoryCacheProvider : IDisposable
    {
        public IMemoryCache MemoryCache { get; }
        private readonly ServiceProvider _serviceProvider;

        public MemoryCacheProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMemoryCache();
            
            _serviceProvider = serviceCollection.BuildServiceProvider();

            MemoryCache = _serviceProvider.GetService<IMemoryCache>();
        }
        
        public void Dispose()
        {
            _serviceProvider?.Dispose();
        }
    }
}