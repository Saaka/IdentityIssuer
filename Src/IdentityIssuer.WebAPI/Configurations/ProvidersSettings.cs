using IdentityIssuer.Application.Configuration;
using Microsoft.Extensions.Configuration;

namespace IdentityIssuer.WebAPI.Configurations
{
    public class ProvidersSettings : IGoogleConfiguration, IFacebookConfiguration
    {
        private readonly IConfiguration _configuration;

        public ProvidersSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GoogleValidationEndpoint => 
            _configuration[ConfigurationProperties.GoogleValidationEndpointProperty];

        public string FacebookValidationEndpoint =>
            _configuration[ConfigurationProperties.FacebookValidationEndpointProperty];
    }
}