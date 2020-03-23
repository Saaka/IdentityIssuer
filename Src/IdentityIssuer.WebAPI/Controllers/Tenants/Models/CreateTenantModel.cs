namespace IdentityIssuer.WebAPI.Controllers.Tenants.Models
{
    public class CreateTenantModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string AllowedOrigin { get; set; }
        public string TokenSecret { get; set; }
        public int TokenExpirationInMinutes { get; set; }
    }
}