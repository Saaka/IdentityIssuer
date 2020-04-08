namespace IdentityIssuer.Application.Tenants.Models
{
    public class CreateTenantData
    {
        public string Name { get; set; }
        public string TenantCode { get; set; }
        public string AllowedOrigin { get; set; }
    }
}