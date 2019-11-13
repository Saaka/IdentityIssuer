using System;
using FluentAssertions;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Infrastructure.Images;
using IdentityIssuer.UnitTests.Utils.Fixtures;
using Moq;
using Xunit;

namespace IdentityIssuer.Infrastructure.UnitTests.Images
{
    [Trait("Infrastructure", "Images")]
    public class GravatarProfileImageUrlProviderTests : IDisposable
    {
        private readonly Fixture fixture;

        public GravatarProfileImageUrlProviderTests()
        {
            fixture = new Fixture();
        }

        [Fact] 
        public void GetImageUrl_Should_Return_Valid_Url_With_Hash()
        {
            var sut = fixture
                .WithHash("abcdefgh")
                .Configure();

            var result = sut.GetImageUrl("thomas.anderson@gmail.com");

            result.Should()
                .StartWith("https://www.gravatar.com/avatar/")
                .And
                .Contain("abcdefgh")
                .And
                .EndWith("?s=96");
        }

        private class Fixture : AutoMockFixture
        {
            private string _hash;

            public Fixture WithHash(string value)
            {
                _hash = value;
                return this;
            }

            public GravatarProfileImageUrlProvider Configure()
            {
                AutoMock.Mock<IHashGenerator>()
                    .Setup(x => x.Generate(It.IsAny<string>()))
                    .Returns(_hash);

                return AutoMock.Create<GravatarProfileImageUrlProvider>();
            }
        }

        public void Dispose()
        {
            fixture?.Dispose();
        }
    }
}