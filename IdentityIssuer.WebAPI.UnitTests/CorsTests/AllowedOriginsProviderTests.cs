using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.WebAPI.Cors;
using IdentityIssuer.WebAPI.UnitTests.Utils;
using Moq;
using Xunit;

namespace IdentityIssuer.WebAPI.UnitTests.CorsTests
{
    [Trait("WebAPI", "Cors")]
    public class AllowedOriginsProviderTests : IDisposable
    {
        private readonly Fixture fixture;

        public AllowedOriginsProviderTests()
        {
            fixture = new Fixture();
        }
        
        [Fact]
        public async Task IsOriginAvailable_Should_Return_True_For_Allowed_Origin()
        {
            var sut = fixture
                .WithAllowedOrigin("http://test.com")
                .Configure();

            var result = await sut.IsOriginAvailable("http://test.com");

            result.Should().BeTrue();
        }
        
        [Fact]
        public async Task IsOriginAvailable_Should_Return_False_For_Not_Allowed_Origin()
        {
            var sut = fixture
                .WithAllowedOrigin("http://test.com")
                .Configure();

            var result = await sut.IsOriginAvailable("http://forbidden.com");

            result.Should().BeFalse();
        }

        public class Fixture : MemoryCacheFixture
        {
            private readonly List<string> allowedOrigins = new List<string>();
            public Fixture WithAllowedOrigin(string origin)
            {
                allowedOrigins.Add(origin);
                return this;
            }

            public AllowedOriginsProvider Configure()
            {
                AutoMock.Mock<ITenantsRepository>()
                    .Setup(x => x.GetAllAllowedOrigins())
                    .ReturnsAsync(allowedOrigins);

                return AutoMock.Create<AllowedOriginsProvider>();
            }
        }

        public void Dispose()
        {
            fixture?.Dispose();
        }
    }
}