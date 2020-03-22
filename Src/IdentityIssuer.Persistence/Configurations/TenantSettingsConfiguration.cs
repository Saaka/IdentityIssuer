using IdentityIssuer.Common.Constants;
using IdentityIssuer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityIssuer.Persistence.Configurations
{
    public class TenantSettingsConfiguration: IEntityTypeConfiguration<TenantSettingsEntity>
    {
        public void Configure(EntityTypeBuilder<TenantSettingsEntity> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.TokenSecret)
                .IsRequired()
                .HasMaxLength(TenantConstants.TokenSecretMaxLength);

            builder
                .Property(x => x.TokenExpirationInMinutes)
                .IsRequired();

            builder
                .HasMany(x => x.TenantProviders)
                .WithOne(x => x.TenantSettings)
                .HasForeignKey(x => x.TenantSettingsId)
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