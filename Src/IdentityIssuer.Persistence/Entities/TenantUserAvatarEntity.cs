using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Persistence.Entities
{
    public class TenantUserAvatarEntity
    {
        public int Id { get; set; }
        public int TenantUserId { get; set; }
        public AvatarType AvatarType { get; set; }
        public string Url { get; set; }

        public virtual TenantUserEntity User { get; set; }
    }
}