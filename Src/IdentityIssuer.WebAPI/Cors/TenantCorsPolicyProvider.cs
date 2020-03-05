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
            string origin;
            if (policyName == PolicyConstants.PreflightPolicy)
                origin = context.Request.Headers[IdentityIssuerHeaders.OriginHeader];
            else
                origin = await _tenantOriginProvider.GetAllowedOrigin(policyName);

            return new CorsPolicyBuilder(origin)
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .Build();
        }
    }
}
