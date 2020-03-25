using IdentityIssuer.Common.Constants;
using IdentityIssuer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityIssuer.Persistence.Configurations
{
    public class TenantConfiguration : IEntityTypeConfiguration<TenantEntity>
    {
        public void Configure(EntityTypeBuilder<TenantEntity> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(TenantConstants.TenantNameMaxLength);

            builder
                .Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(TenantConstants.TenantCodeMaxLength);

            builder
                .HasIndex(x => x.Code)
                .IsUnique()
                .HasName("IX_TenantCode");

            builder
                .HasMany(x => x.Users)
                .WithOne(x => x.Tenant)
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.TenantSettings)
                .WithOne(x => x.Tenant)
                .HasForeignKey<TenantSettingsEntity>(x => x.TenantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.TenantProviders)
                .WithOne(x => x.Tenant)
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
