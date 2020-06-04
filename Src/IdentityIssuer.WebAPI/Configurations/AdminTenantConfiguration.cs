using System;
using IdentityIssuer.Application.Configuration;
using IdentityIssuer.Common.Constants;
using IdentityIssuer.Common.Enums;
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

        public string TenantName =>
            _configuration[AdminTenantConfigurationProperties.TenantName];

        public string TenantCode =>
            _configuration[AdminTenantConfigurationProperties.TenantCode];

        public string UserDisplayName =>
            _configuration[AdminTenantConfigurationProperties.UserDisplayName];

        public string Email =>
            _configuration[AdminTenantConfigurationProperties.Email];

        public string Password =>
            _configuration[AdminTenantConfigurationProperties.Password];

        public string AllowedOrigin =>
            _configuration[AdminTenantConfigurationProperties.AllowedOrigin];

        public string TokenSecret =>
            _configuration[AdminTenantConfigurationProperties.TokenSecret];

        public int TokenExpirationInMinutes =>
            int.Parse(_configuration[AdminTenantConfigurationProperties.TokenExpirationInMinutes] ??
                      TenantConstants.DefaultTokenExpirationInMinutes.ToString());

        public LanguageCode DefaultLanguage =>
            Enum.TryParse(_configuration[AdminTenantConfigurationProperties.DefaultLanguage],
                out LanguageCode defaultLanguage)
                ? defaultLanguage
                : LanguageCode.EN;
    }
}