using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;
using IdentityIssuer.WebAPI.Configurations;

namespace IdentityIssuer.WebAPI.Cors
{
    public class TenantCorsMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantCorsMiddleware(
            RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context,
            ICorsService corsService,
            ICorsPolicyProvider policyProvider,
            IAllowedOriginsProvider allowedOriginsProvider)
        {
            if (context.Request.Headers.ContainsKey(CorsConstants.Origin))
            {
                var accessControlRequestMethod = context.Request.Headers[CorsConstants.AccessControlRequestMethod];
                var isPreflight = IsPreflight(context, accessControlRequestMethod);

                var corsPolicy = await GetPolicy(context, isPreflight, policyProvider, allowedOriginsProvider);

                if (corsPolicy != null)
                {
                    var corsResult = corsService.EvaluatePolicy(context, corsPolicy);
                    corsService.ApplyResult(corsResult, context.Response);

                    if (isPreflight)
                    {
                        context.Response.StatusCode = StatusCodes.Status204NoContent;
                        return;
                    }
                }
            }

            await _next(context);
        }

        private async Task<CorsPolicy> GetPolicy(HttpContext context, bool isPreflight, ICorsPolicyProvider policyProvider, IAllowedOriginsProvider allowedOriginsProvider)
        {
            if (isPreflight && await allowedOriginsProvider.IsOriginAvailable(context.Request.Headers[IdentityIssuerHeaders.OriginHeader]))
                return await policyProvider.GetPolicyAsync(context, PolicyConstants.PreflightPolicy);
            else if (context.Request.Headers.ContainsKey(IdentityIssuerHeaders.TenantHeader))
                return await policyProvider.GetPolicyAsync(context, context.Request.Headers[IdentityIssuerHeaders.TenantHeader]);
            else
                return null;
        }

        private bool IsPreflight(HttpContext context, StringValues accessControlRequestMethod)
        {
            return string.Equals(context.Request.Method,
                               CorsConstants.PreflightHttpMethod,
                               StringComparison.OrdinalIgnoreCase) &&
                               !StringValues.IsNullOrEmpty(accessControlRequestMethod);
        }
    }
}