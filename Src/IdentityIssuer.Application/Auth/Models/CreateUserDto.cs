using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Auth.Models
{
    public class CreateUserDto
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string UserGuid { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }
        public string ExternalUserId { get; set; }
        public int TenantId { get; set; }
        public AvatarType AvatarType { get; set; }
    }
}