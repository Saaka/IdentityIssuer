using System;
using Autofac.Extras.Moq;

namespace IdentityIssuer.WebAPI.UnitTests.Utils
{
    /// <summary>
    /// Base fixture with AutoMock
    /// </summary>
    public abstract class AutoMockFixture : IDisposable
    {
        protected AutoMock AutoMock { get; }

        public AutoMockFixture()
        {
            AutoMock = AutoMock.GetLoose();
        }
        
        public virtual void Dispose()
        {
            AutoMock?.Dispose();
        }
    }
}