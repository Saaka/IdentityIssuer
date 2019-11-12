using IdentityIssuer.Application.Services;
using IdentityIssuer.Infrastructure.Cache;
using IdentityIssuer.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityIssuer.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services
                .AddTransient<ICacheStore, MemoryCacheStore>();
            
            services
                .AddTransient<IJwtTokenFactory, JwtTokenFactory>();
            
            return services;
        }
    }
}