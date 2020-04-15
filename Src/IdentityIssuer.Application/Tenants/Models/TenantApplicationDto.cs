using System;

namespace IdentityIssuer.Application.Tenants.Models
{
    public class TenantApplicationDto
    {
        public Guid TenantApplicationGuid { get; set; }
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