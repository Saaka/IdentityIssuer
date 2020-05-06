namespace IdentityIssuer.Application.Tenants.Models
{
    public class CreateTenantData
    {
        public string Name { get; set; }
        public string TenantCode { get; set; }
        public bool IsAdminTenant { get; set; }
    }
}