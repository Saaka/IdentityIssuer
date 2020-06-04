using IdentityIssuer.Common.Enums;
using IdentityIssuer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityIssuer.Persistence.Configurations
{
    public class MailTemplateConfiguration : IEntityTypeConfiguration<MailTemplateEntity>
    {
        public void Configure(EntityTypeBuilder<MailTemplateEntity> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder
                .Property(x => x.Language)
                .IsRequired()
                .HasConversion(
                    v => (byte) v,
                    v => (LanguageCode) v);
            
            builder
                .Property(x => x.MessageType)
                .IsRequired()
                .HasConversion(
                    v => (byte) v,
                    v => (MailMessageType) v);

            builder.Property(x => x.Template)
                .IsRequired();
        }
    }
}