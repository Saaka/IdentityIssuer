using IdentityIssuer.Application;
using IdentityIssuer.Persistence;
using IdentityIssuer.WebAPI.Configurations;
using IdentityIssuer.WebAPI.Cors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
                .AddPersistenceModule()
                .AddApplicationModule()
                .AddExternalServices()
                .AddCors()
                .AddMvcWithFilters();
        }

        public void Configure(IApplicationBuilder application, IHostingEnvironment env)
        {
            if(env.IsProduction())
            {
                application
                    .UseHsts()
                    .UseHttpsRedirection();
            }

            application
                .UseMiddleware<TenantCorsMiddleware>()
                .UseAuthentication()
                .UseMvc();
        }
    }
}
