using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using IdentityIssuer.Application;
using IdentityIssuer.Application.Behaviors;
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
                    typeof(Persistence.PersistenceMapperProfile).Assembly
                })
                .AddMediatRBehaviors()
                .AddMediatR(typeof(ApplicationModule).Assembly)
                .AddMemoryCache();

            return services;
        }
        
        private static IServiceCollection AddMediatRBehaviors(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>))
                
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));

            return services;
        }
    }
}
