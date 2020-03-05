using IdentityIssuer.Application.Configuration;
using Microsoft.Extensions.Configuration;

namespace IdentityIssuer.WebAPI.Configurations
{
    public class AuthSettings : ITokenConfiguration
    {
        private readonly IConfiguration _configuration;

        public AuthSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string Issuer => _configuration[ConfigurationProperties.Issuer];
    }
}