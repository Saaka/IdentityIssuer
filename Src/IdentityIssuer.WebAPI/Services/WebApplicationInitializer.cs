using System;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Persistence.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IdentityIssuer.WebAPI.Services
{
    public class WebApplicationInitializer
    {
        public static void Initialize(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<WebApplicationInitializer>>();
                try
                {
                    logger.LogInformation("Initializing database");
                    var initializer = services.GetRequiredService<IDbInitializer>();
                    initializer.ExecuteAsync()
                        .GetAwaiter().GetResult();
                    logger.LogInformation("Database initialization successful");

                    logger.LogInformation("Initializing tenant from configuration");
                    var tenantInitializer = services.GetRequiredService<ITenantInitializer>();
                    var tenant = tenantInitializer.InitializeTenantFromConfigurationAsync()
                        .GetAwaiter().GetResult();
                    if (tenant != null)
                        logger.LogInformation("Tenant initialization successful");
                    else 
                        logger.LogInformation("Tenant initialization failed");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while seeding the database.");
                    throw;
                }
            }
        }
    }
}