using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using IdentityIssuer.Application;
using MediatR;

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
                .AddMediatR(typeof(ApplicationModule).Assembly)
                .AddMemoryCache();

            return services;
        }
    }
}
