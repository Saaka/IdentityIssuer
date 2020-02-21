using IdentityIssuer.Application.Services;

namespace IdentityIssuer.Infrastructure.Images
{
    public class GravatarProfileImageUrlProvider : IProfileImageUrlProvider
    {
        private const string ServiceAddress = "https://www.gravatar.com/avatar/";
        private const string DefaultSizeParam = "?s=96";

        private readonly IHashGenerator hashGenerator;

        public GravatarProfileImageUrlProvider(IHashGenerator hashGenerator)
        {
            this.hashGenerator = hashGenerator;
        }

        public string GetImageUrl(string email)
        {
            var emailHash = hashGenerator.Generate(email);
            if (string.IsNullOrWhiteSpace(emailHash))
                return null;

            return $"{ServiceAddress}{emailHash}{DefaultSizeParam}";
        }
    }
}