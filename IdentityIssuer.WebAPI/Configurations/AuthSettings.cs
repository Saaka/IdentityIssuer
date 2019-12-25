using IdentityIssuer.Application.Configuration;
using Microsoft.Extensions.Configuration;

namespace IdentityIssuer.WebAPI.Configurations
{
    public class AuthSettings : ITokenConfiguration
    {
        private readonly IConfiguration configuration;

        public AuthSettings(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public string Issuer => configuration[ConfigurationProperties.Issuer];
    }
}