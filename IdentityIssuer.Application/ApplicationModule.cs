using IdentityIssuer.Application.Services;
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
            return services;
        }
    }
}