using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands
{
    public class UpdateTenantSettingsCommand : Request<TenantSettingsDto>
    {
        public string TenantCode { get; }
        public string TokenSecret { get; }
        public int TokenExpirationInMinutes { get; }
        public bool EnableCredentialsLogin { get; }
        public bool EnableGoogleLogin { get; }
        public bool EnableFacebookLogin { get; }
        public AdminContextData AdminContextData { get; private set; }

        public UpdateTenantSettingsCommand(
            string tenantCode, string tokenSecret, int tokenExpirationInMinutes,
            bool enableCredentialsLogin, bool enableGoogleLogin, bool enableFacebookLogin)
        {
            TenantCode = tenantCode;
            TokenSecret = tokenSecret;
            TokenExpirationInMinutes = tokenExpirationInMinutes;
            EnableCredentialsLogin = enableCredentialsLogin;
            EnableGoogleLogin = enableGoogleLogin;
            EnableFacebookLogin = enableFacebookLogin;
        }

        public UpdateTenantSettingsCommand WithAdminContextData(AdminContextData value)
        {
            AdminContextData = value;
            return this;
        }
    }
}