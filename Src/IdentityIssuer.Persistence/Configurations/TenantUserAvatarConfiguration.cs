using IdentityIssuer.Common.Constants;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityIssuer.Persistence.Configurations
{
    public class TenantUserAvatarConfiguration : IEntityTypeConfiguration<TenantUserAvatarEntity>
    {
        public void Configure(EntityTypeBuilder<TenantUserAvatarEntity> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Avatars)
                .HasForeignKey(x => x.TenantUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(e => e.Url)
                .IsRequired()
                .HasMaxLength(UserConstants.MaxImageUrlLength);

            builder
                .Property(x => x.AvatarType)
                .IsRequired()
                .HasConversion(
                    v => (byte) v,
                    v => (AvatarType) v);

            builder
                .HasIndex(x => new {x.TenantUserId, x.AvatarType})
                .IsUnique()
                .HasName("IX_TenantUserId_AvatarType");
        }
    }
}