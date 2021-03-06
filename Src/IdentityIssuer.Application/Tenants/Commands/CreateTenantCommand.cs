using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Common.Enums;
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
        public LanguageCode DefaultLanguage { get; }
        public bool IsAdminTenant { get; }

        public CreateTenantCommand(
            string name,
            string tenantCode,
            string allowedOrigin,
            string tokenSecret, 
            int tokenExpirationInMinutes,
            bool enableCredentialsLogin,
            bool enableGoogleLogin,
            bool enableFacebookLogin,
            LanguageCode defaultLanguage,
            bool isAdminTenant)
        {
            Name = name;
            TenantCode = tenantCode;
            AllowedOrigin = allowedOrigin;
            TokenSecret = tokenSecret;
            TokenExpirationInMinutes = tokenExpirationInMinutes;
            EnableCredentialsLogin = enableCredentialsLogin;
            EnableGoogleLogin = enableGoogleLogin;
            EnableFacebookLogin = enableFacebookLogin;
            DefaultLanguage = defaultLanguage;
            IsAdminTenant = isAdminTenant;
        }
    }
}