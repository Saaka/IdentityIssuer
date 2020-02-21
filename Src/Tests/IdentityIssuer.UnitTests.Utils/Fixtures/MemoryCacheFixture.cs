namespace IdentityIssuer.UnitTests.Utils.Fixtures
{
    /// <summary>
    /// Base fixture using AutoMock and MemoryCacheProvider that enables to use MemoryCache
    /// </summary>
    public abstract class MemoryCacheFixture : AutoMockFixture
    {
        private MemoryCacheProvider MemoryCacheProvider { get; }

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