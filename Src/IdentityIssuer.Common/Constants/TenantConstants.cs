namespace IdentityIssuer.Common.Constants
{
    public static class TenantConstants
    {
        public const int ProviderIdentifierMaxLength = 128;
        public const int ProviderKeyMaxLength = 128;
        public const int TenantNameMaxLength = 32;
        public const int TenantCodeMaxLength = 3;
        public const int TenantAllowedOriginMaxLength = 128;
        public const int TokenSecretMaxLength = 256;

        public const int DefaultTokenExpirationInMinutes = 10080;
    }
}
