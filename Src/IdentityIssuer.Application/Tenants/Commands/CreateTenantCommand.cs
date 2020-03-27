using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands
{
    public class CreateTenantCommand : Request<TenantDto>
    {
        public string Name { get; }
        public string TenantCode { get; }
        public string AllowedOrigin { get; }
        public string TokenSecret { get; }
        public int TokenExpirationInMinutes { get; }
        public bool EnableCredentialsLogin { get; }
        public bool EnableGoogleLogin { get; }
        public bool EnableFacebookLogin { get; }
        public AdminContextData AdminContextData { get; private set; }

        public CreateTenantCommand(
            string name,
            string tenantCode,
            string allowedOrigin,
            string tokenSecret, 
            int tokenExpirationInMinutes,
            bool enableCredentialsLogin,
            bool enableGoogleLogin,
            bool enableFacebookLogin)
        {
            Name = name;
            TenantCode = tenantCode;
            AllowedOrigin = allowedOrigin;
            TokenSecret = tokenSecret;
            TokenExpirationInMinutes = tokenExpirationInMinutes;
            EnableCredentialsLogin = enableCredentialsLogin;
            EnableGoogleLogin = enableGoogleLogin;
            EnableFacebookLogin = enableFacebookLogin;
        }

        public CreateTenantCommand WithAdminContextData(AdminContextData contextData)
        {
            AdminContextData = contextData;
            return this;
        }
    }
}