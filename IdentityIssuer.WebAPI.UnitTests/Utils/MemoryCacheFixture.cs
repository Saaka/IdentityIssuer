using System;

namespace IdentityIssuer.WebAPI.UnitTests.Utils
{
    /// <summary>
    /// Base fixture using AutoMock and MemoryCacheProvider that enables to use MemoryCache
    /// </summary>
    public abstract class MemoryCacheFixture : AutoMockFixture
    {
        protected MemoryCacheProvider MemoryCacheProvider { get; }
        public MemoryCacheFixture()
        {    
            MemoryCacheProvider = new MemoryCacheProvider();
            AutoMock.Provide(MemoryCacheProvider.MemoryCache);
        }
        
        public override void Dispose()
        {
            MemoryCacheProvider?.Dispose();
            base.Dispose();
        }
    }
}