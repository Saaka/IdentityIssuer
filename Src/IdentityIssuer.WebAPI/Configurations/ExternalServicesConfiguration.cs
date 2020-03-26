using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using IdentityIssuer.Application;
using IdentityIssuer.Application.Configuration;
using IdentityIssuer.Persistence;
using IdentityIssuer.WebAPI.Controllers.Tenants.Models;
using MediatR;
using MediatR.Pipeline;

namespace IdentityIssuer.WebAPI.Configurations
{
    public static class ExternalServicesConfiguration
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            services
                .AddAutoMapper(new Assembly[]
                {
                    typeof(PersistenceMapperProfile).Assembly,
                    typeof(Infrastructure.InfrastructureAutoMapperProfile).Assembly,
                    typeof(ApplicationMapperProfile).Assembly,
                    typeof(ApiTenantMapperProfile).Assembly,
                })
                .AddMemoryCache()
                .AddMediatRBehaviors()
                .AddMediatR(typeof(ApplicationModule).Assembly);

            return services;
        }
        
        private static IServiceCollection AddMediatRBehaviors(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>))

                .AddPersistenceModuleBehaviors()
                .AddApplicationModuleBehaviors();
            
            return services;
        }
    }
}
