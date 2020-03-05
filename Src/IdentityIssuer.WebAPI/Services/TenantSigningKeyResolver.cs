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
            var currentTenant = _contextDataProvider.GetTenant(_httpContextAccessor.HttpContext).Result;
            if (currentTenant == null)
                throw new UnauthorizedAccessException(ExceptionCode.MissingTenantContextData.ToString());
            if (currentTenant.TenantCode != kid)
                throw new UnauthorizedAccessException(ExceptionCode.KidMissmatch.ToString());

            var tenantSettings = _tenantProvider.GetTenantSettings(currentTenant.TenantCode);
            if (string.IsNullOrEmpty(tenantSettings?.TokenSecret))
                throw new UnauthorizedAccessException(ExceptionCode.MissingTenantTokenSecret.ToString());

            return new[]
            {
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tenantSettings.TokenSecret))
            };
        }
    }
}