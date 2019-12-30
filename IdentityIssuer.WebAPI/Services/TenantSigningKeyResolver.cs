using System;
using System.Collections.Generic;
using System.Text;
using IdentityIssuer.Application.Tenants;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace IdentityIssuer.WebAPI.Services
{
    public interface ITenantSigningKeyResolver
    {
        IEnumerable<SecurityKey> ResolveSecurityKey(
            string token,
            SecurityToken securityToken,
            string kid,
            TokenValidationParameters validationParameters);
    }

    public class TenantSigningKeyResolver : ITenantSigningKeyResolver
    {
        private readonly ITenantProvider tenantProvider;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IContextDataProvider contextDataProvider;

        public TenantSigningKeyResolver(
            ITenantProvider tenantProvider,
            IHttpContextAccessor httpContextAccessor,
            IContextDataProvider contextDataProvider)
        {
            this.tenantProvider = tenantProvider;
            this.httpContextAccessor = httpContextAccessor;
            this.contextDataProvider = contextDataProvider;
        }

        public IEnumerable<SecurityKey> ResolveSecurityKey(
            string token,
            SecurityToken securityToken,
            string kid,
            TokenValidationParameters validationParameters)
        {
            var currentTenant = contextDataProvider.GetTenant(httpContextAccessor.HttpContext).Result;
            if (currentTenant.TenantCode != kid)
                throw new UnauthorizedAccessException(currentTenant.TenantCode);
            var key = tenantProvider.GetTenantSettings(currentTenant.TenantCode).TokenSecret;
            
            return new[]
            {
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };
        }
    }
}