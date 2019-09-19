using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityIssuer.Persistence
{
    public class AppIdentityContextFactory : IDesignTimeDbContextFactory<AppIdentityContext>
    {
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public AppIdentityContextFactory() { }

        public AppIdentityContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable(AspNetCoreEnvironment);

            throw new NotImplementedException();
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
