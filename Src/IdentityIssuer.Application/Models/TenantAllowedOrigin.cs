namespace IdentityIssuer.Application.Models
{
    public class TenantAllowedOrigin
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string AllowedOrigin { get; set; }
    }
}