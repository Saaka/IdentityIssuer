using System.Text;
using IdentityIssuer.WebAPI.Filters;
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
                })
                .AddJsonOptions(s => s.UseCamelCasing(true))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }

        public static IServiceCollection AddJwtTokenBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
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
                            return new [] { new SymmetricSecurityKey(Encoding.UTF8.GetBytes(""))};
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
