using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace IdentityIssuer.WebAPI.Cors
{
    public class TenantCorsPolicyProvider : ICorsPolicyProvider
    {
        private readonly ITenantOriginProvider tenantOriginProvider;

        public TenantCorsPolicyProvider(ITenantOriginProvider tenantOriginProvider)
        {
            this.tenantOriginProvider = tenantOriginProvider;
        }

        public async Task<CorsPolicy> GetPolicyAsync(HttpContext context, string policyName)
        {
            string origin;
            if (policyName == PolicyConstants.TenantHeader.ToLower())
                origin = "*";
            else
                origin = await tenantOriginProvider.GetAllowedOrigin(policyName);

            return new CorsPolicyBuilder(origin)
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .Build();
        }
    }
}
