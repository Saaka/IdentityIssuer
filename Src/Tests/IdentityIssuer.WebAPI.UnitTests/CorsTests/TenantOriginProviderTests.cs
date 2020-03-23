using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly Fixture _fixture;

        public TenantOriginProviderTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetAllowedOrigin_Should_Return_Origin_If_Tenant_Exists()
        {
            var sut = _fixture
                .WithTenantOrigins("http://test.com")
                .Configure();

            var origin = await sut.GetAllowedOrigin("Code");

            origin.Should().Contain("http://test.com");
        }

        [Fact]
        public async Task GetAllowedOrigin_Should_Return_Null_If_Tenant_Not_Exists()
        {
            var sut = _fixture
                .Configure();

            var origin = await sut.GetAllowedOrigin("Code");

            origin.Should().BeNull();
        }

        public class Fixture : AutoMockFixture
        {
            private List<string> _allowedOrigins;

            public Fixture WithTenantOrigins(params string[] values)
            {
                _allowedOrigins = values.ToList();
                return this;
            }

            public TenantOriginProvider Configure()
            {
                AutoMockInstance.Mock<ICacheStore>()
                    .Setup(x => x.GetOrCreateAsync(It.IsAny<string>(), It.IsAny<Func<ICacheEntry, Task<List<string>>>>()))
                    .ReturnsAsync(_allowedOrigins);

                return AutoMockInstance.Create<TenantOriginProvider>();
            }
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }
    }
}