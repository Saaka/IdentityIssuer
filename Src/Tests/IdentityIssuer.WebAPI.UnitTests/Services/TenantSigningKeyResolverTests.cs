using System;
using Autofac.Extras.Moq;
using FluentAssertions;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests.RequestContexts;
using IdentityIssuer.WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Xunit;

namespace IdentityIssuer.WebAPI.UnitTests.Services
{
    [Trait("WebAPI", "Services")]
    public class TenantSigningKeyResolverTests
    {
        [Fact]
        public void ResolveSecurityKey_Should_Return_Collection_With_One_SymmetricSecurityKey()
        {
            var sut = new Fixture()
                .WithTenantSettings(secret: "123")
                .WithContextData(tenantCode: "TST")
                .Configure();

            var result = sut.ResolveSecurityKey(
                null,
                null,
                "TST",
                null);

            result
                .Should()
                .HaveCount(1)
                .And
                .AllBeAssignableTo(typeof(SymmetricSecurityKey));
        }

        [Fact]
        public void ResolveSecurityKey_Should_Throw_UnauthorizedException_For_Kid_Tenant_Missmatch()
        {
            var sut = new Fixture()
                .WithTenantSettings(secret: "123")
                .WithContextData(tenantCode: "TST")
                .Configure();

            sut.Invoking(x => x.ResolveSecurityKey(
                    null,
                    null,
                    "ABC",
                    null))
                .Should()
                .Throw<UnauthorizedAccessException>()
                .And
                .Message
                .Should()
                .Be(ErrorCode.KidMissmatch.ToString());
        }

        [Fact]
        public void ResolveSecurityKey_Should_Throw_UnauthorizedException_For_Missing_Settings()
        {
            var sut = new Fixture()
                .WithContextData(tenantCode: "TST")
                .Configure();

            sut.Invoking(x => x.ResolveSecurityKey(
                    null,
                    null,
                    "TST",
                    null))
                .Should()
                .Throw<UnauthorizedAccessException>()
                .And
                .Message
                .Should()
                .Be(ErrorCode.MissingTenantTokenSecret.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ResolveSecurityKey_Should_Throw_UnauthorizedException_For_Missing_Secret(string secret)
        {
            var sut = new Fixture()
                .WithTenantSettings(secret)
                .WithContextData(tenantCode: "TST")
                .Configure();

            sut.Invoking(x => x.ResolveSecurityKey(
                    null,
                    null,
                    "TST",
                    null))
                .Should()
                .Throw<UnauthorizedAccessException>()
                .And
                .Message
                .Should()
                .Be(ErrorCode.MissingTenantTokenSecret.ToString());
        }

        [Fact]
        public void ResolveSecurityKey_Should_Throw_UnauthorizedException_For_Missing_Context_Data()
        {
            var sut = new Fixture()
                .WithTenantSettings("123")
                .Configure();

            sut.Invoking(x => x.ResolveSecurityKey(
                    null,
                    null,
                    "TST",
                    null))
                .Should()
                .Throw<UnauthorizedAccessException>()
                .And
                .Message
                .Should()
                .Be(ErrorCode.MissingTenantContext.ToString());
        }

        private class Fixture
        {
            public Mock<ITenantProvider> TenantProviderMock { get; private set; }
            public Mock<IContextDataProvider> ContextDataProviderMock { get; private set; }
            public Mock<IHttpContextAccessor> HttpContextAccessorMock { get; private set; }

            private TenantSettings _tenantSettings;
            public Fixture WithTenantSettings(string secret)
            {
                _tenantSettings = new TenantSettings
                {
                    TokenSecret = secret
                };
                return this;
            }

            private RequestContext _context;
            public Fixture WithContextData(string tenantCode)
            {
                _context = new RequestContext()
                    .WithTenantContext(new TenantContext(1, tenantCode, false));
                return this;
            }

            public TenantSigningKeyResolver Configure()
            {
                using (var mock = AutoMock.GetLoose())
                {
                    TenantProviderMock = mock.Mock<ITenantProvider>();
                    TenantProviderMock.Setup(x => x.GetTenantSettings(It.IsAny<string>()))
                        .Returns(_tenantSettings);

                    ContextDataProviderMock = mock.Mock<IContextDataProvider>();
                    ContextDataProviderMock.Setup(x => x.GetRequestContext(It.IsAny<HttpContext>()))
                        .ReturnsAsync(_context);

                    HttpContextAccessorMock = mock.Mock<IHttpContextAccessor>();
                    HttpContextAccessorMock.Setup(x => x.HttpContext)
                        .Returns((HttpContext) null);

                    return mock.Create<TenantSigningKeyResolver>();
                }
            }
        }
    }
}