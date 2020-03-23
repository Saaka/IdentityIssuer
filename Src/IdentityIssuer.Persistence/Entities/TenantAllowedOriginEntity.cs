namespace IdentityIssuer.Persistence.Entities
{
    public class TenantAllowedOriginEntity
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string AllowedOrigin { get; set; }
        
        public virtual TenantEntity Tenant { get; set; }
    }
}