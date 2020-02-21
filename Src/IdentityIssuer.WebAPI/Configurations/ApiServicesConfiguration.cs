using IdentityIssuer.Application.Configuration;
using IdentityIssuer.WebAPI.Cors;
using IdentityIssuer.WebAPI.Services;
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
                .AddTransient<ICorsPolicyProvider, TenantCorsPolicyProvider>()
                .AddTransient<IAllowedOriginsProvider, AllowedOriginsProvider>()
                
                .AddTransient<IContextDataProvider, ContextDataProvider>()
                
                .AddTransient<ITokenConfiguration, AuthSettings>()
                
                .AddTransient<IGoogleConfiguration, ProvidersSettings>()
                .AddTransient<IFacebookConfiguration, ProvidersSettings>()
                
                .AddTransient<ITenantSigningKeyResolver, TenantSigningKeyResolver>();

            return services;
        }
    }
}
