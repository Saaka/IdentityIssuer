﻿using IdentityIssuer.Persistence.Configurations;
using IdentityIssuer.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityIssuer.Persistence
{
    public class AppIdentityContext
        : IdentityDbContext<TenantUserEntity, IdentityRole<int>, int,
            IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public AppIdentityContext() { }
        public AppIdentityContext(DbContextOptions options) : base(options) { }

        public DbSet<TenantEntity> Tenants { get; set; }
        public DbSet<TenantConfigurationEntity> TenantConfigurations { get; set; }
        public DbSet<TenantProviderEntity> TenantProviders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(PersistenceConstants.DefaultIdentitySchema);
            builder.ApplyConfigurationsFromAssembly(typeof(AppIdentityContext).Assembly,
                x => x.Namespace == typeof(TenantUserConfiguration).Namespace);
        }
    }
}
