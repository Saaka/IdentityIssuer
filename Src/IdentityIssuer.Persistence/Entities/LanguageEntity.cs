using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Persistence.Entities
{
    public class LanguageEntity
    {
        public int Id { get; set; }
        public LanguageCode Code { get; set; }
    }
}