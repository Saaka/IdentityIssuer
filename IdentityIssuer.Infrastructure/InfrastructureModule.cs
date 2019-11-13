using IdentityIssuer.Application.Services;
using IdentityIssuer.Infrastructure.Cache;
using IdentityIssuer.Infrastructure.Helpers;
using IdentityIssuer.Infrastructure.Http;
using IdentityIssuer.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityIssuer.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {
            services
                .AddTransient<IHashGenerator, HashGenerator>()
                .AddTransient<IGuid, GuidProvider>()
                .AddTransient<IDateTime, UtcDateProvider>()
                    
                .AddTransient<ICacheStore, MemoryCacheStore>()

                .AddTransient<IRestSharpClientFactory, RestSharpClientFactory>()
            
                .AddTransient<IJwtTokenFactory, JwtTokenFactory>();
            
            return services;
        }
    }
}