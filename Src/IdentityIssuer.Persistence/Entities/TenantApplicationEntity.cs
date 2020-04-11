using System;

namespace IdentityIssuer.Persistence.Entities
{
    public class TenantApplicationEntity
    {
        public int Id { get; set; }
        public Guid TenantApplicationGuid { get; set; }
        public string Name { get; set; }
        public string TenantCode { get; set; }
        public string OwnerEmail { get; set; }
        public string AllowedOrigin { get; set; }
        public string TokenSecret { get; set; }
        public int TokenExpirationInMinutes { get; set; }
        public bool EnableCredentialsLogin { get; set; }
        public bool EnableGoogleLogin { get; set; }
        public bool EnableFacebookLogin { get; set; }
        public bool TenantCreated { get; set; }
        public int? TenantId { get; set; }
        
        public virtual TenantEntity Tenant { get; set; }
    }
}