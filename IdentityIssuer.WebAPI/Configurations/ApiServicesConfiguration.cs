using IdentityIssuer.WebAPI.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityIssuer.WebAPI.Configurations
{
    public static class ApiServicesConfiguration
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services
                .AddTransient<ITenantOriginProvider, TenantOriginProvider>()
                .AddTransient<ICorsPolicyProvider, TenantCorsPolicyProvider>();

            return services;
        }
    }
}
