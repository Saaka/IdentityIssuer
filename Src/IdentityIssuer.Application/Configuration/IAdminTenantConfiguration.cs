using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Configuration
{
    public interface IAdminTenantConfiguration
    {
        string TenantName { get; }
        string TenantCode { get; }
        string UserDisplayName { get; }
        string Email { get; }
        string Password { get; }
        string AllowedOrigin { get; }
        string TokenSecret { get; }
        int TokenExpirationInMinutes { get; }
        LanguageCode DefaultLanguage { get; }
    }
}