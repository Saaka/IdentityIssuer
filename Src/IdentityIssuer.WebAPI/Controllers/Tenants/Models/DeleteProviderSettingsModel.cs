using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.WebAPI.Controllers.Tenants.Models
{
    public class DeleteProviderSettingsModel
    {
        public string TenantCode { get; set; }
        public AuthProviderType ProviderType { get; set; }
    }
}