using IdentityIssuer.Domain.Constants;
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
                .HasMaxLength(ValidationConstants.TenantNameMaxLength);

            builder
                .Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(ValidationConstants.TenantCodeMaxLength);

            builder
                .HasIndex(x => x.Code)
                .IsUnique()
                .HasName("IX_TenantCode");

            builder
                .Property(x => x.AllowedOrigin)
                .IsRequired()
                .HasMaxLength(ValidationConstants.TenantAllowedOriginMaxLength);

            builder
                .HasMany(x => x.Users)
                .WithOne(x => x.Tenant)
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
