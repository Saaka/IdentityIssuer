using IdentityIssuer.Common.Constants;
using IdentityIssuer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityIssuer.Persistence.Configurations
{
    public class TenantApplicationConfiguration : IEntityTypeConfiguration<TenantApplicationEntity>
    {
        public void Configure(EntityTypeBuilder<TenantApplicationEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.TenantApplicationGuid)
                .IsRequired();

            builder
                .HasIndex(x => x.TenantApplicationGuid)
                .HasName("IX_TenantApplication_TenantApplicationGuid")
                .IncludeProperties(x => x.Id)
                .IsUnique();
            
            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(TenantConstants.TenantNameMaxLength);

            builder
                .Property(x => x.TenantCode)
                .IsRequired()
                .HasMaxLength(TenantConstants.TenantCodeMaxLength);
            
            builder
                .Property(x => x.AllowedOrigin)
                .IsRequired()
                .HasMaxLength(TenantConstants.TenantAllowedOriginMaxLength);

            builder
                .Property(x => x.TokenSecret)
                .IsRequired()
                .HasMaxLength(TenantConstants.TokenSecretMaxLength);

            builder
                .Property(x => x.TokenExpirationInMinutes)
                .IsRequired();

            builder
                .Property(x => x.EnableCredentialsLogin)
                .IsRequired();
            
            builder
                .Property(x => x.EnableFacebookLogin)
                .IsRequired();
            
            builder
                .Property(x => x.EnableGoogleLogin)
                .IsRequired();

            builder
                .Property(x => x.OwnerEmail)
                .IsRequired()
                .HasMaxLength(UserConstants.MaxEmailLength);

            builder
                .Property(x => x.TenantCreated)
                .HasDefaultValue(false);

            builder
                .Property(x => x.TenantId)
                .IsRequired(false);
        }
    }
}