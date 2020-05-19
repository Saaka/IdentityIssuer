using System;
using System.Collections.Generic;
using System.Text;
using IdentityIssuer.Application.Tenants;
using IdentityIssuer.Common.Enums;
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
        private readonly ITenantProvider _tenantProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContextDataProvider _contextDataProvider;

        public TenantSigningKeyResolver(
            ITenantProvider tenantProvider,
            IHttpContextAccessor httpContextAccessor,
            IContextDataProvider contextDataProvider)
        {
            _tenantProvider = tenantProvider;
            _httpContextAccessor = httpContextAccessor;
            _contextDataProvider = contextDataProvider;
        }

        public IEnumerable<SecurityKey> ResolveSecurityKey(
            string token,
            SecurityToken securityToken,
            string kid,
            TokenValidationParameters validationParameters)
        {
            var requestContext = _contextDataProvider.GetRequestContext(_httpContextAccessor.HttpContext).Result;
            if (requestContext == null || !requestContext.IsTenantContext)
                throw new UnauthorizedAccessException(ErrorCode.MissingTenantContext.ToString());
            if (requestContext.Tenant.TenantCode != kid)
                throw new UnauthorizedAccessException(ErrorCode.KidMissmatch.ToString());

            var tenantSettings = _tenantProvider.GetTenantSettings(requestContext.Tenant.TenantCode);
            if (string.IsNullOrEmpty(tenantSettings?.TokenSecret))
                throw new UnauthorizedAccessException(ErrorCode.MissingTenantTokenSecret.ToString());

            return new[]
            {
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tenantSettings.TokenSecret))
            };
        }
    }
}