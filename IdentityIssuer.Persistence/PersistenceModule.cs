using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Persistence.Repositories;
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
                ;

            return services;
        }
        
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(PersistenceConstants.AppConnectionString);
            RegisterContext<AppIdentityContext>(services, connectionString, PersistenceConstants.MigrationsTable);

            return services;
        }

        private static void RegisterContext<T>(IServiceCollection services, string connectionString, string migrationsTable)
            where T : DbContext
        {
            services.AddDbContext<T>((opt) =>
                    opt.UseSqlServer(
                        connectionString,
                        cb =>
                        {
                            cb.MigrationsHistoryTable(migrationsTable);
                        }),
                ServiceLifetime.Scoped);
        }
    }
}