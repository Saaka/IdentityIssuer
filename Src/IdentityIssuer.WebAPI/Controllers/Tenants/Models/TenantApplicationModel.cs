namespace IdentityIssuer.WebAPI.Controllers.Tenants.Models
{
    public class TenantApplicationModel
    {
        public string Name { get; set; }
        public string TenantCode { get; set; }
        public string AllowedOrigin { get; set; }
        public string TokenSecret { get; set; }
        public int TokenExpirationInMinutes { get; set; }
        public bool EnableCredentialsLogin { get; set; }
        public bool EnableGoogleLogin { get; set; }
        public bool EnableFacebookLogin { get; set; }
        public string OwnerEmail { get; set; }
    }
}