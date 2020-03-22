using System;
using System.Threading.Tasks;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Persistence.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IdentityIssuer.WebAPI.Services
{
    public class WebApplicationInitializer
    {
        public  static async Task InitializeAsync(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<WebApplicationInitializer>>();
                try
                {
                    logger.LogInformation("Initializing database");
                    
                    var initializer = services.GetRequiredService<IDbInitializer>();
                    await initializer.ExecuteAsync();
                    
                    logger.LogInformation("Database initialization successful");

                    logger.LogInformation("Initializing tenant from configuration");
                    
                    var tenantInitializer = services.GetRequiredService<ITenantInitializer>();
                    var tenant = await tenantInitializer.InitializeTenantFromConfigurationAsync();
                    
                    logger.LogInformation(tenant != null
                        ? "Tenant initialization successful"
                        : "Tenant initialization failed");
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