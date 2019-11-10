namespace IdentityIssuer.Application.Tenants
{
    public class TenantConfiguration
    {
        public int Id { get; set; }
        public string TokenSecret { get; set; }
        public int TokenExpirationInMinutes { get; set; }
        public bool EnableCredentialsLogin { get; set; }
        public bool EnableGoogleLogin { get; set; }
        public bool EnableFacebookLogin { get; set; }
    }
}