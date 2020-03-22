using IdentityIssuer.Common.Constants;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityIssuer.Persistence.Configurations
{
    public class TenantProviderConfiguration : IEntityTypeConfiguration<TenantProviderSettingsEntity>
    {
        public void Configure(EntityTypeBuilder<TenantProviderSettingsEntity> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.ProviderType)
                .IsRequired()
                .HasConversion(
                    v => (byte) v,
                    v => (AuthProviderType) v);

            builder
                .Property(x => x.Identifier)
                .IsRequired()
                .HasMaxLength(TenantConstants.ProviderIdentifierMaxLength);
            
            builder
                .Property(x => x.Key)
                .IsRequired()
                .HasMaxLength(TenantConstants.ProviderKeyMaxLength);
        }
    }
}