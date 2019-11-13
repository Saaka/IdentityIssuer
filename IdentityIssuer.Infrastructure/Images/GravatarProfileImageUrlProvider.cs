using IdentityIssuer.Application.Services;
using IdentityIssuer.Infrastructure.Helpers;

namespace IdentityIssuer.Infrastructure.Images
{
    public class GravatarProfileImageUrlProvider : IProfileImageUrlProvider
    {
        private const string ServiceAddress = "https://www.gravatar.com/avatar/";
        private const string SizeParam = "?s=96";

        private readonly HashGenerator hashGenerator;

        public GravatarProfileImageUrlProvider(HashGenerator hashGenerator)
        {
            this.hashGenerator = hashGenerator;
        }

        public string GetImageUrl(string email)
        {
            var emailHash = hashGenerator.Generate(email);
            if (string.IsNullOrWhiteSpace(emailHash))
                return null;

            return $"{ServiceAddress}{emailHash}{SizeParam}";
        }
    }
}