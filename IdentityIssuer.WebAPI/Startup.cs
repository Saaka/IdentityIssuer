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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddApiServices()
                .AddExternalServices()
                .AddCors()
                .AddMvcWithFilters();
        }

        public void Configure(IApplicationBuilder application, IHostingEnvironment env)
        {
            if(env.IsProduction())
            {
                application
                    .UseHsts();
            }

            application
                .UseMiddleware<TenantCorsMiddleware>()
                .UseHttpsRedirection()
                .UseMvc();
        }
    }
}
