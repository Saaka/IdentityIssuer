using System.Collections.Generic;

namespace IdentityIssuer.WebAPI.Controllers.Tenants.Models
{
    public class UpdateTenantSettingsModel
    {
        public string TenantCode { get; set; }
        public string TokenSecret { get; set; }
        public int TokenExpirationInMinutes { get; set; }
        public bool EnableCredentialsLogin { get; set; }
        public bool EnableGoogleLogin { get; set; }
        public bool EnableFacebookLogin { get; set; }
        public List<string> AllowedOrigins { get; set; }
        
        public UpdateTenantSettingsModel()
        {
            AllowedOrigins= new List<string>();
        }
    }
}