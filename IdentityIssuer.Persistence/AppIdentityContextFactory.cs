using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace IdentityIssuer.Persistence
{
    public class AppIdentityContextFactory : IDesignTimeDbContextFactory<AppIdentityContext>
    {
        public AppIdentityContextFactory() { }

        public AppIdentityContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable(PersistenceConstants.AspNetCoreEnvironment);
            var connectionString = GetConnectionString(environment);

            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"Connection string '{PersistenceConstants.AppConnectionString}' is null or empty.", nameof(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder<AppIdentityContext>();
            optionsBuilder.UseSqlServer(connectionString,
                opt => opt.MigrationsHistoryTable(PersistenceConstants.MigrationsTable));

            return new AppIdentityContext(optionsBuilder.Options);
        }

        private string GetConnectionString(string environmentName)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Local.json", optional: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            return configuration.GetConnectionString(PersistenceConstants.AppConnectionString);
        }
    }
}
