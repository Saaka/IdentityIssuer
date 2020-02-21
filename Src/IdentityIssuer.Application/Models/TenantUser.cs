namespace IdentityIssuer.Application.Models
{
    public class TenantUser
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string UserGuid { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAdmin { get; set; }
    }
}