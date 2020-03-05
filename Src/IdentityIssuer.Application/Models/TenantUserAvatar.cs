using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Models
{
    public class TenantUserAvatar
    {
        public int Id { get; set; }
        public AvatarType AvatarType { get; set; }
        public int TenantUserId { get; set; }
        public string ImageUrl { get; set; }
    }
}