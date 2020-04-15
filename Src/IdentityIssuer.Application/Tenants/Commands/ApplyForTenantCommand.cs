using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands
{
    public class ApplyForTenantCommand : Request<TenantApplicationDto>
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
        public TenantContextData Tenant { get; private set; }

        public ApplyForTenantCommand(string name, string tenantCode, string allowedOrigin, string tokenSecret,
            int tokenExpirationInMinutes, bool enableCredentialsLogin, bool enableGoogleLogin, bool enableFacebookLogin,
            string ownerEmail)
        {
            Name = name;
            TenantCode = tenantCode;
            AllowedOrigin = allowedOrigin;
            TokenSecret = tokenSecret;
            TokenExpirationInMinutes = tokenExpirationInMinutes;
            EnableCredentialsLogin = enableCredentialsLogin;
            EnableGoogleLogin = enableGoogleLogin;
            EnableFacebookLogin = enableFacebookLogin;
            OwnerEmail = ownerEmail;
        }

        public ApplyForTenantCommand WithTenantContextData(TenantContextData tenant)
        {
            Tenant = tenant;
            return this;
        }
    }
}