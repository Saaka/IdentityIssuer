using Autofac;

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
        }

        protected override void Register(ContainerBuilder builder)
        {
            builder.RegisterInstance(MemoryCacheProvider.MemoryCache);
        }

        public override void Dispose()
        {
            MemoryCacheProvider?.Dispose();
            base.Dispose();
        }
    }
}