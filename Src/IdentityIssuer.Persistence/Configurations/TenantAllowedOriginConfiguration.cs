using IdentityIssuer.Common.Constants;
using IdentityIssuer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityIssuer.Persistence.Configurations
{
    public class TenantAllowedOriginConfiguration: IEntityTypeConfiguration<TenantAllowedOriginEntity>
    {
        public void Configure(EntityTypeBuilder<TenantAllowedOriginEntity> builder)
        {
            builder
                .HasKey(x => x.Id);
            
            builder
                .HasOne(x => x.Tenant)
                .WithMany(x => x.AllowedOrigins)
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder
                .Property(x => x.AllowedOrigin)
                .IsRequired()
                .HasMaxLength(TenantConstants.TenantAllowedOriginMaxLength);
        }
    }
}