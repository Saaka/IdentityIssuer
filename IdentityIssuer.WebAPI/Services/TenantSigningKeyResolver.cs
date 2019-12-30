using System;
using System.Collections.Generic;
using System.Text;
using IdentityIssuer.Application.Tenants;
using IdentityIssuer.Common.Constants;
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
            if (currentTenant == null)
                throw new UnauthorizedAccessException(
                    Exceptions.UnauthorizedAccessException.MissingTenantContextData);
            if (currentTenant.TenantCode != kid)
                throw new UnauthorizedAccessException(
                    Exceptions.UnauthorizedAccessException.KidMissmatch);

            var tenantSettings = tenantProvider.GetTenantSettings(currentTenant.TenantCode);
            if (string.IsNullOrEmpty(tenantSettings?.TokenSecret))
                throw new UnauthorizedAccessException(Exceptions.UnauthorizedAccessException.MissingTenantTokenSecret);

            return new[]
            {
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tenantSettings.TokenSecret))
            };
        }
    }
}