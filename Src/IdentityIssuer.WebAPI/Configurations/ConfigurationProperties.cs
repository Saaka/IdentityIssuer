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
        public const string Name = "AdminTenant:Name";
        public const string Code = "AdminTenant:Code";
        public const string Email = "AdminTenant:Email";
        public const string Password = "AdminTenant:Password";
        public const string AllowedOrigin = "AdminTenant:AllowedOrigin";
    }
}