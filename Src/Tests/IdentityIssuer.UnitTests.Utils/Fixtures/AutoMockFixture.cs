using System;
using Autofac;
using Autofac.Extras.Moq;

namespace IdentityIssuer.UnitTests.Utils.Fixtures
{
    /// <summary>
    /// Base fixture with AutoMock
    /// </summary>
    public abstract class AutoMockFixture : IDisposable
    {
        protected AutoMock AutoMockInstance { get; }

        public AutoMockFixture()
        {
            AutoMockInstance = AutoMock.GetLoose(Register);
        }

        protected virtual void Register(ContainerBuilder builder)
        {
        }

        public virtual void Dispose()
        {
            AutoMockInstance?.Dispose();
        }
    }
}