using System.Collections.Generic;

namespace IdentityIssuer.Persistence.Entities
{
    public class TenantSettingsEntity
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string TokenSecret { get; set; }
        public int TokenExpirationInMinutes { get; set; }
        public bool EnableCredentialsLogin { get; set; }
        public bool EnableGoogleLogin { get; set; }
        public bool EnableFacebookLogin { get; set; }

        public virtual TenantEntity Tenant { get; set; }
        public virtual ICollection<TenantProviderSettingsEntity> TenantProviders { get; set; } = new List<TenantProviderSettingsEntity>();
    }
}