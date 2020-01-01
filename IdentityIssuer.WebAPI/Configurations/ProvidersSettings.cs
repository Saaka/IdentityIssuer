using IdentityIssuer.Application.Configuration;
using Microsoft.Extensions.Configuration;

namespace IdentityIssuer.WebAPI.Configurations
{
    public class ProvidersSettings : IGoogleConfiguration
    {
        private readonly IConfiguration configuration;

        public ProvidersSettings(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string ValidationEndpoint => configuration[ConfigurationProperties.GoogleValidationEndpointProperty];
    }
}