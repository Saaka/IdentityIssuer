using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Persistence.Entities
{
    public class MailTemplateEntity
    {
        public int Id { get; set; }
        public MailMessageType MessageType { get; set; }
        public LanguageCode Language { get; set; }
        public string Template { get; set; }
    }
}