﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using IdentityIssuer.Application;
using IdentityIssuer.Application.Configuration;
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
                    typeof(Persistence.PersistenceMapperProfile).Assembly,
                    typeof(Infrastructure.InfrastructureAutoMapperProfile).Assembly,
                    typeof(ApplicationMapperProfile).Assembly
                })
                .AddMemoryCache()
                .AddMediatrBehaviors()
                .AddMediatR(typeof(ApplicationModule).Assembly);

            return services;
        }
        
        private static IServiceCollection AddMediatrBehaviors(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>))

                .AddApplicationModuleBehaviors();
            
            
            return services;
        }
    }
}
