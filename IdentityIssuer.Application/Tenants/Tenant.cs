namespace IdentityIssuer.Application.Tenants
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string AllowedOrigin { get; set; }
    }
}
