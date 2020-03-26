namespace IdentityIssuer.Application.Tenants.Models
{
    public class TenantSettingsDto
    {
        public string TokenSecret { get; set; }
        public int TokenExpirationInMinutes { get; set; }
        public bool EnableCredentialsLogin { get; set; }
        public bool EnableGoogleLogin { get; set; }
        public bool EnableFacebookLogin { get; set; }
    }
}