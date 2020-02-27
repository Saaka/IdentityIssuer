using System;
using System.Diagnostics;
using IdentityIssuer.Application;
using IdentityIssuer.Infrastructure;
using IdentityIssuer.Persistence;
using IdentityIssuer.WebAPI.Configurations;
using IdentityIssuer.WebAPI.Cors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityIssuer.WebAPI
{
    public class Startup
    {
        IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddApiServices()
                .AddDbContext(Configuration)
                .AddIdentityStore(Configuration)
                .AddPersistenceModule()
                .AddApplicationModule()
                .AddInfrastructureModule()
                .AddExternalServices()
                .AddCors()
                .AddMvcWithFilters()
                .AddJwtTokenBearerAuthentication(Configuration);
        }

        public void Configure(IApplicationBuilder application, IWebHostEnvironment env)
        {
            if(env.IsProduction())
            {
                application
                    .UseHsts()
                    .UseHttpsRedirection();
            }

            application
                .UseMiddleware<TenantCorsMiddleware>()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(e => { e.MapControllers();});
            
            Console.WriteLine($"Welcome to IdentityIssuer. PID: {Process.GetCurrentProcess().Id.ToString()}");
        }
    }
}
