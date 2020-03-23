using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using IdentityIssuer.WebAPI.Configurations;

namespace IdentityIssuer.WebAPI.Cors
{
    public class TenantCorsPolicyProvider : ICorsPolicyProvider
    {
        private readonly ITenantOriginProvider _tenantOriginProvider;

        public TenantCorsPolicyProvider(ITenantOriginProvider tenantOriginProvider)
        {
            _tenantOriginProvider = tenantOriginProvider;
        }

        public async Task<CorsPolicy> GetPolicyAsync(HttpContext context, string policyName)
        {
            var origins = policyName == PolicyConstants.PreflightPolicy
                ? new string[] { context.Request.Headers[IdentityIssuerHeaders.OriginHeader] }
                : (await _tenantOriginProvider.GetAllowedOrigin(policyName)).ToArray();

            return new CorsPolicyBuilder(origins)
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .Build();
        }
    }
}