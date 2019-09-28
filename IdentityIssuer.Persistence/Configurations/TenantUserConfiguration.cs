using IdentityIssuer.Common.Constants;
using IdentityIssuer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityIssuer.Persistence.Configurations
{
    public class TenantUserConfiguration : IEntityTypeConfiguration<TenantUserEntity>
    {
        public void Configure(EntityTypeBuilder<TenantUserEntity> builder)
        {
            builder
                .HasOne(x => x.Tenant)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasIndex(x => x.UserGuid)
                .HasName("IX_UserGuid")
                .IsUnique();

            builder
                .Property(x => x.UserGuid)
                .IsRequired()
                .HasMaxLength(ValidationConstants.GuidMaxLength);

            builder
                .Property(e => e.GoogleId)
                .IsRequired(false)
                .HasMaxLength(64);

            builder
                .Property(e => e.FacebookId)
                .IsRequired(false)
                .HasMaxLength(64);

            builder
                .Property(e => e.DisplayName)
                .IsRequired(true)
                .HasMaxLength(128);

            builder
                .Property(e => e.ImageUrl)
                .IsRequired(false)
                .HasMaxLength(1024);
        }
    }
}
