namespace IdentityIssuer.Application.Models
{
    public class TenantUser
    {
        public string DisplayName { get; set; }
        public string UserGuid { get; set; }
        public bool IsAdmin { get; set; }
    }
}