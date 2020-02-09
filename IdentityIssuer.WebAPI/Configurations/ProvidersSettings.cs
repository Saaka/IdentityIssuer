using IdentityIssuer.Application.Configuration;
using Microsoft.Extensions.Configuration;

namespace IdentityIssuer.WebAPI.Configurations
{
    public class ProvidersSettings : IGoogleConfiguration, IFacebookConfiguration
    {
        private readonly IConfiguration configuration;

        public ProvidersSettings(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GoogleValidationEndpoint => 
            configuration[ConfigurationProperties.GoogleValidationEndpointProperty];

        public string FacebookValidationEndpoint =>
            configuration[ConfigurationProperties.FacebookValidationEndpointProperty];
    }
}