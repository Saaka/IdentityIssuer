using IdentityIssuer.Application.Behaviors;
using IdentityIssuer.Application.Tenants;
using IdentityIssuer.Application.Users;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityIssuer.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services
                .AddTransient<ITenantProvider, TenantProvider>()
                .AddTransient<IUsersProvider, UsersProvider>();

            return services;
        }

        public static IServiceCollection AddApplicationModuleBehaviors(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLogger<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));

            return services;
        }
    }
}