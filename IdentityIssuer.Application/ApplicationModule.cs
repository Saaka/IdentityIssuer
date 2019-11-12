using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityIssuer.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services
                .AddTransient<ITenantProvider, TenantProvider>();
            
            return services;
        }
    }
}