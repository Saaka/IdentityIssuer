using IdentityIssuer.Application.Auth.Repositories;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Application.Users.Repositories;
using IdentityIssuer.Common.Constants;
using IdentityIssuer.Persistence.Behaviors;
using IdentityIssuer.Persistence.Entities;
using IdentityIssuer.Persistence.Repositories;
using IdentityIssuer.Persistence.Utilities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityIssuer.Persistence
{
    public static class PersistenceModule
    {
        public static IServiceCollection AddPersistenceModule(this IServiceCollection services)
        {
            services
                .AddTransient<ITenantsRepository, TenantsRepository>()
                .AddTransient<ITenantSettingsRepository, TenantSettingsRepository>()
                .AddTransient<ITenantProviderSettingsRepository, TenantProviderSettingsRepository>()
                .AddTransient<ITenantAllowedOriginsRepository, TenantAllowedOriginsRepository>()
                .AddTransient<ITenantApplicationsRepository, TenantApplicationsRepository>()
                .AddTransient<IUserRepository, UsersRepository>()
                .AddTransient<IAuthRepository, AuthRepository>()
                .AddTransient<IAvatarRepository, AvatarRepository>()
                
                .AddTransient<IDbInitializer, DbInitializer>()
                .AddTransient<ITenantInitializer, TenantInitializer>();

            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(PersistenceConstants.AppConnectionString);
            RegisterContext<AppIdentityContext>(services, connectionString, PersistenceConstants.MigrationsTable);

            return services;
        }

        private static void RegisterContext<T>(IServiceCollection services, string connectionString,
            string migrationsTable)
            where T : DbContext
        {
            services.AddDbContext<T>((opt) =>
                    opt.UseSqlServer(
                        connectionString,
                        cb => { cb.MigrationsHistoryTable(migrationsTable); }),
                ServiceLifetime.Scoped);
        }

        public static IServiceCollection AddIdentityStore(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddIdentityCore<TenantUserEntity>(opt =>
                {
                    opt.User = new UserOptions
                    {
                        AllowedUserNameCharacters = UserConstants.AllowedUserNameCharacters,
                        RequireUniqueEmail = false
                    };
                    opt.Password = new PasswordOptions
                    {
                        RequireDigit = false,
                        RequireUppercase = false,
                        RequireNonAlphanumeric = false,
                    };
                })
                .AddRoles<IdentityRole<int>>()
                .AddUserStore<UserStore<TenantUserEntity, IdentityRole<int>, AppIdentityContext, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityUserToken<int>, IdentityRoleClaim<int>>>()
                .AddRoleStore<RoleStore<IdentityRole<int>, AppIdentityContext, int, IdentityUserRole<int>, IdentityRoleClaim<int>>>();

            return services;
        }

        public static IServiceCollection AddPersistenceModuleBehaviors(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionScopeBehavior<,>));

            return services;
        }
    }
}