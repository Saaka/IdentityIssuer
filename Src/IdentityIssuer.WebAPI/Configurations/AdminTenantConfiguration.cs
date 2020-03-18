using IdentityIssuer.Application.Configuration;
using Microsoft.Extensions.Configuration;

namespace IdentityIssuer.WebAPI.Configurations
{
    public class AdminTenantConfiguration : IAdminTenantConfiguration
    {
        private readonly IConfiguration _configuration;

        public AdminTenantConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Name   => 
            _configuration[AdminTenantConfigurationProperties.Name];
        public string Code  => 
            _configuration[AdminTenantConfigurationProperties.Code];
        public string Email=> 
            _configuration[AdminTenantConfigurationProperties.Email];
        public string Password => 
            _configuration[AdminTenantConfigurationProperties.Password];
        public string AllowedOrigin => 
            _configuration[AdminTenantConfigurationProperties.AllowedOrigin];
    }
}