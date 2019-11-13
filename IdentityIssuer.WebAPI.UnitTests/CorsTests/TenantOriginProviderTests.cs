using System;
using System.Threading.Tasks;
using FluentAssertions;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Services;
using IdentityIssuer.UnitTests.Utils.Fixtures;
using IdentityIssuer.WebAPI.Cors;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace IdentityIssuer.WebAPI.UnitTests.CorsTests
{
    [Trait("WebAPI", "Cors")]
    public class TenantOriginProviderTests : IDisposable
    {
        private readonly Fixture fixture;

        public TenantOriginProviderTests()
        {
            fixture = new Fixture();
        }

        [Fact]
        public async Task GetAllowedOrigin_Should_Return_Origin_If_Tenant_Exists()
        {
            var sut = fixture
                .WithTenant(new Tenant
                {
                    AllowedOrigin = "http://test.com"
                })
                .Configure();

            var origin = await sut.GetAllowedOrigin("Code");

            origin.Should().Be("http://test.com");
        }

        [Fact]
        public async Task GetAllowedOrigin_Should_Return_Null_If_Tenant_Not_Exists()
        {
            var sut = fixture
                .Configure();

            var origin = await sut.GetAllowedOrigin("Code");

            origin.Should().BeNull();
        }

        public class Fixture : AutoMockFixture
        {
            Tenant tenant;

            public Fixture WithTenant(Tenant tenant)
            {
                this.tenant = tenant;
                return this;
            }

            public TenantOriginProvider Configure()
            {
                AutoMock.Mock<ICacheStore>()
                    .Setup(x => x.GetOrCreateAsync(It.IsAny<string>(), It.IsAny<Func<ICacheEntry, Task<string>>>()))
                    .ReturnsAsync(tenant?.AllowedOrigin);

                return AutoMock.Create<TenantOriginProvider>();
            }
        }

        public void Dispose()
        {
            fixture?.Dispose();
        }
    }
}