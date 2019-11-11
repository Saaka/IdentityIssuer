using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants;
using IdentityIssuer.Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityIssuer.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services
                .AddTransient<ICacheStore, MemoryCacheStore>();
            
            services
                .AddTransient<ITenantProvider, TenantProvider>();
            
            return services;
        }
    }
}