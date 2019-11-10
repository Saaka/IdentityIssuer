using IdentityIssuer.Common.Constants;
using IdentityIssuer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityIssuer.Persistence.Configurations
{
    public class TenantConfigurationConfiguration: IEntityTypeConfiguration<TenantConfigurationEntity>
    {
        public void Configure(EntityTypeBuilder<TenantConfigurationEntity> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.TokenSecret)
                .IsRequired()
                .HasMaxLength(ValidationConstants.TokenSecretMaxLength);

            builder
                .Property(x => x.TokenExpirationInMinutes)
                .IsRequired();

            builder
                .HasMany(x => x.TenantProviders)
                .WithOne(x => x.TenantConfiguration)
                .HasForeignKey(x => x.TenantConfigurationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(x => x.EnableCredentialsLogin)
                .IsRequired();
            
            builder
                .Property(x => x.EnableFacebookLogin)
                .IsRequired();
            
            builder
                .Property(x => x.EnableGoogleLogin)
                .IsRequired();
        }
    }
}