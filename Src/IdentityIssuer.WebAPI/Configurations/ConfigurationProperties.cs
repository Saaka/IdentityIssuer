namespace IdentityIssuer.WebAPI.Configurations
{
    public static class ConfigurationProperties
    {
        public const string Issuer = "Auth:Issuer";
        public const string GoogleValidationEndpointProperty = "Google:ValidationEndpoint";
        public const string FacebookValidationEndpointProperty = "Facebook:ValidationEndpoint";
    }

    public static class AdminTenantConfigurationProperties
    {
        public const string TenantName = "AdminTenant:TenantName";
        public const string TenantCode = "AdminTenant:TenantCode";
        public const string UserDisplayName = "AdminTenant:UserDisplayName";
        public const string Email = "AdminTenant:Email";
        public const string Password = "AdminTenant:Password";
        public const string AllowedOrigin = "AdminTenant:AllowedOrigin";
        public const string TokenSecret = "AdminTenant:TokenSecret";
        public const string TokenExpirationInMinutes = "AdminTenant:TokenExpirationInMinutes";
        public const string DefaultLanguage = "AdminTenant:DefaultLanguage";
    }
}