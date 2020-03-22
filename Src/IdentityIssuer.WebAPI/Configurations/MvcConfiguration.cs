using FluentValidation.AspNetCore;
using IdentityIssuer.Application;
using IdentityIssuer.WebAPI.Pipeline;
using IdentityIssuer.WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace IdentityIssuer.WebAPI.Configurations
{
    public static class MvcConfiguration
    {
        public static IServiceCollection AddMvcWithFilters(this IServiceCollection services)
        {
            services
                .AddControllers(options =>
                {
                    options.Filters.Add<CustomExceptionFilterAttribute>();
                    options.Filters.Add<TenantActionFilter>();
                })
                .AddFluentValidation(v =>
                {
                    v.RegisterValidatorsFromAssembly(typeof(ApplicationModule).Assembly);
                    v.LocalizationEnabled = false;
                    v.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; })
                .AddHttpContextAccessor();

            return services;
        }

        public static IServiceCollection AddJwtTokenBearerAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            var tenantKeyResolver = serviceProvider.GetService<ITenantSigningKeyResolver>();

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKeyResolver = tenantKeyResolver.ResolveSecurityKey,

                        ValidateIssuer = true,
                        ValidIssuer = configuration[ConfigurationProperties.Issuer],

                        ValidateAudience = false
                    };
                });

            return services;
        }
    }
}