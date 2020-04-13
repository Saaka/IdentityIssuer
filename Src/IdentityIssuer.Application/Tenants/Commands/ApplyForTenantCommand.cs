using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands
{
    public class ApplyForTenantCommand : Request
    {
        public string Name { get; }
        public string TenantCode { get; }
        public string AllowedOrigin { get; }
        public string TokenSecret { get; }
        public int TokenExpirationInMinutes { get; }
        public bool EnableCredentialsLogin { get; }
        public bool EnableGoogleLogin { get; }
        public bool EnableFacebookLogin { get; }
        public string OwnerEmail { get; }
        public string OwnerPassword { get; }
        public TenantContextData Tenant { get; }
    }
}