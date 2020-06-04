using IdentityIssuer.Common.Enums;
using IdentityIssuer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityIssuer.Persistence.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<LanguageEntity>
    {
        public void Configure(EntityTypeBuilder<LanguageEntity> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder
                .Property(x => x.Code)
                .IsRequired()
                .HasConversion(
                    v => (byte) v,
                    v => (LanguageCode) v);

            builder.HasIndex(x => x.Code)
                .IsUnique()
                .HasName("IX_Language_Code");
        }
    }
}