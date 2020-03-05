using System;
using Autofac.Extras.Moq;
using FluentAssertions;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants;
using IdentityIssuer.Common.Enums;
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
            var fixture = new Fixture()
                .WithTenantSettings(secret: "123")
                .WithContextData(tenantCode: "TST");
            var sut = fixture.Configure();

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
            var fixture = new Fixture()
                .WithTenantSettings(secret: "123")
                .WithContextData(tenantCode: "TST");
            var sut = fixture.Configure();

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
                .Be(ExceptionCode.KidMissmatch.ToString());
        }

        [Fact]
        public void ResolveSecurityKey_Should_Throw_UnauthorizedException_For_Missing_Settings()
        {
            var fixture = new Fixture()
                .WithContextData(tenantCode: "TST");
            var sut = fixture.Configure();

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
                .Be(ExceptionCode.MissingTenantTokenSecret.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ResolveSecurityKey_Should_Throw_UnauthorizedException_For_Missing_Secret(string secret)
        {
            var fixture = new Fixture()
                .WithTenantSettings(secret)
                .WithContextData(tenantCode: "TST");
            var sut = fixture.Configure();

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
                .Be(ExceptionCode.MissingTenantTokenSecret.ToString());
        }

        [Fact]
        public void ResolveSecurityKey_Should_Throw_UnauthorizedException_For_Missing_Context_Data()
        {
            var fixture = new Fixture()
                .WithTenantSettings("123");
            var sut = fixture.Configure();

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
                .Be(ExceptionCode.MissingTenantContextData.ToString());
        }

        private class Fixture
        {
            public TenantSigningKeyResolver Sut { get; private set; }
            public Mock<ITenantProvider> TenantProviderMock { get; private set; }
            public Mock<IContextDataProvider> ContextDataProviderMock { get; private set; }
            public Mock<IHttpContextAccessor> HttpContextAccessorMock { get; private set; }

            private TenantSettings tenantSettings;

            public Fixture WithTenantSettings(string secret)
            {
                tenantSettings = new TenantSettings
                {
                    TokenSecret = secret
                };
                return this;
            }

            private TenantContextData tenantContextData;

            public Fixture WithContextData(string tenantCode)
            {
                tenantContextData = new TenantContextData(1, tenantCode);
                return this;
            }

            public TenantSigningKeyResolver Configure()
            {
                using (var mock = AutoMock.GetLoose())
                {
                    TenantProviderMock = mock.Mock<ITenantProvider>();
                    TenantProviderMock.Setup(x => x.GetTenantSettings(It.IsAny<string>()))
                        .Returns(tenantSettings);

                    ContextDataProviderMock = mock.Mock<IContextDataProvider>();
                    ContextDataProviderMock.Setup(x => x.GetTenant(It.IsAny<HttpContext>()))
                        .ReturnsAsync(tenantContextData);

                    HttpContextAccessorMock = mock.Mock<IHttpContextAccessor>();
                    HttpContextAccessorMock.Setup(x => x.HttpContext)
                        .Returns((HttpContext) null);

                    return Sut = mock.Create<TenantSigningKeyResolver>();
                }
            }
        }
    }
}