using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IdentityIssuer.WebAPI.Configurations
{
    public static class ExternalServicesConfiguration
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            services
                .AddAutoMapper(new Assembly[]
                {
                    typeof(Persistence.PersistenceMapperProfile).Assembly
                })
                .AddMemoryCache();

            return services;
        }
    }
}
