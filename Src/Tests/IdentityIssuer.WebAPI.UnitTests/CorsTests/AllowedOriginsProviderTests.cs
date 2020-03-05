using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using IdentityIssuer.Application.Services;
using IdentityIssuer.UnitTests.Utils.Fixtures;
using IdentityIssuer.WebAPI.Cors;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace IdentityIssuer.WebAPI.UnitTests.CorsTests
{
    [Trait("WebAPI", "Cors")]
    public class AllowedOriginsProviderTests : IDisposable
    {
        private readonly Fixture _fixture;

        public AllowedOriginsProviderTests()
        {
            _fixture = new Fixture();
        }
        
        [Fact]
        public async Task IsOriginAvailable_Should_Return_True_For_Allowed_Origin()
        {
            var sut = _fixture
                .WithAllowedOrigin("http://test.com")
                .Configure();

            var result = await sut.IsOriginAvailable("http://test.com");

            result.Should().BeTrue();
        }
        
        [Fact]
        public async Task IsOriginAvailable_Should_Return_False_For_Not_Allowed_Origin()
        {
            var sut = _fixture
                .WithAllowedOrigin("http://test.com")
                .Configure();

            var result = await sut.IsOriginAvailable("http://forbidden.com");

            result.Should().BeFalse();
        }

        public class Fixture : AutoMockFixture
        {
            private readonly List<string> _allowedOrigins = new List<string>();
            public Fixture WithAllowedOrigin(string origin)
            {
                _allowedOrigins.Add(origin);
                return this;
            }

            public AllowedOriginsProvider Configure()
            {
                AutoMockInstance.Mock<ICacheStore>()
                    .Setup(x => x.GetOrCreateAsync(It.IsAny<string>(), It.IsAny<Func<ICacheEntry, Task<IEnumerable<string>>>>()))
                    .ReturnsAsync(_allowedOrigins);
                
                return AutoMockInstance.Create<AllowedOriginsProvider>();
            }
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }
    }
}