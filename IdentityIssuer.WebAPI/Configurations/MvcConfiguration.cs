using System;
using System.Text;
using FluentValidation.AspNetCore;
using IdentityIssuer.Application;
using IdentityIssuer.Application.Tenants;
using IdentityIssuer.WebAPI.Pipeline;
using IdentityIssuer.WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
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
                .AddMvc(options =>
                {
                    options.Filters.Add<CustomExceptionFilterAttribute>();
                    options.Filters.Add<TenantActionFilter>();
                })
                .AddJsonOptions(s => s.UseCamelCasing(true))
                .AddFluentValidation(v => v.RegisterValidatorsFromAssembly(typeof(ApplicationModule).Assembly))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; })
                .AddHttpContextAccessor();

            return services;
        }

        public static IServiceCollection AddJwtTokenBearerAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            var tenantProvider = serviceProvider.GetService<ITenantProvider>();
            var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            var contextDataProvider = serviceProvider.GetService<IContextDataProvider>();

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
                        IssuerSigningKeyResolver = (string token, SecurityToken securityToken, string kid,
                            TokenValidationParameters validationParameters) =>
                        {
                            var currentTenant = contextDataProvider.GetTenant(httpContextAccessor.HttpContext).Result;
                            if (currentTenant.TenantCode != kid)
                                throw new UnauthorizedAccessException(currentTenant.TenantCode);
                            var key = tenantProvider.GetTenantSettings(currentTenant.TenantCode).TokenSecret;
                            return new[]
                            {
                                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                            };
                        },

                        ValidateIssuer = true,
                        ValidIssuer = configuration[ConfigurationProperties.Issuer],

                        ValidateAudience = false
                    };
                });

            return services;
        }
    }
}