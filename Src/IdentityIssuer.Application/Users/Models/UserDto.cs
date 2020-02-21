namespace IdentityIssuer.Application.Users.Models
{
    public class UserDto
    {
        public string UserGuid { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAdmin { get; set; }
    }
}