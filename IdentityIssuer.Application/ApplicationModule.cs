using IdentityIssuer.Application.Tenants;
using IdentityIssuer.Application.Users;
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
    }
}