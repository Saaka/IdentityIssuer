using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.WebAPI.Controllers.Tenants.Models
{
    public class UpdateProviderSettingsModel
    {
        public string TenantCode { get; set; }
        public AuthProviderType ProviderType { get; set; }
        public string Identifier { get; set; }
        public string Key { get; set; }
    }
}