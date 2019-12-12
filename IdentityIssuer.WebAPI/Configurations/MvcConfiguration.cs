using System.Text;
using FluentValidation.AspNetCore;
using IdentityIssuer.Application;
using IdentityIssuer.Application.Tenants;
using IdentityIssuer.WebAPI.Pipeline;
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
                .AddMvc(options =>
                {
                    options.Filters.Add<CustomExceptionFilterAttribute>();
                    options.Filters.Add<TenantActionFilter>();
                })
                .AddJsonOptions(s => s.UseCamelCasing(true))
                .AddFluentValidation(v => v.RegisterValidatorsFromAssembly(typeof(ApplicationModule).Assembly))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            return services;
        }

        public static IServiceCollection AddJwtTokenBearerAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            var tenantProvider = services.BuildServiceProvider().GetService<ITenantProvider>();

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
                        IssuerSigningKeyResolver = (string token, SecurityToken securityToken, string tenantCode,
                            TokenValidationParameters validationParameters) =>
                        {
                            var key = tenantProvider.GetTenantSettings(tenantCode).TokenSecret;
                            return new[] {new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))};
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