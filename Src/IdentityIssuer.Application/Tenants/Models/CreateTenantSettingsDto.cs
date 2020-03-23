namespace IdentityIssuer.Application.Tenants.Models
{
    public class CreateTenantSettingsDto
    {
        public int TenantId { get; }
        public string TokenSecret { get; }
        public int TokenExpirationInMinutes { get; }
        public bool EnableCredentialsLogin { get; }
        public bool EnableGoogleLogin { get; }
        public bool EnableFacebookLogin { get; }

        public CreateTenantSettingsDto(
            int tenantId,
            string tokenSecret,
            int tokenExpirationInMinutes,
            bool enableCredentialsLogin,
            bool enableGoogleLogin,
            bool enableFacebookLogin)
        {
            TenantId = tenantId;
            TokenSecret = tokenSecret;
            TokenExpirationInMinutes = tokenExpirationInMinutes;
            EnableCredentialsLogin = enableCredentialsLogin;
            EnableGoogleLogin = enableGoogleLogin;
            EnableFacebookLogin = enableFacebookLogin;
        }
    }
}