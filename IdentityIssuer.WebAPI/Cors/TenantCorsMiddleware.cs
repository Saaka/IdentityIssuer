using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace IdentityIssuer.WebAPI.Cors
{
    public class TenantCorsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICorsService _corsService;
        private readonly ICorsPolicyProvider _corsPolicyProvider;
        private readonly IAllowedOriginsProvider _allowedOriginsProvider;

        public TenantCorsMiddleware(
            RequestDelegate next,
            ICorsService corsService,
            ICorsPolicyProvider policyProvider,
            IAllowedOriginsProvider allowedOriginsProvider)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _corsService = corsService ?? throw new ArgumentNullException(nameof(corsService));
            _corsPolicyProvider = policyProvider ?? throw new ArgumentNullException(nameof(policyProvider));
            _allowedOriginsProvider = allowedOriginsProvider ?? throw new ArgumentNullException(nameof(allowedOriginsProvider));
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey(CorsConstants.Origin))
            {
                var accessControlRequestMethod = context.Request.Headers[CorsConstants.AccessControlRequestMethod];
                var isPreflight = IsPreflight(context, accessControlRequestMethod);

                var corsPolicy = await GetPolicy(context, isPreflight);

                if (corsPolicy != null)
                {
                    var corsResult = _corsService.EvaluatePolicy(context, corsPolicy);
                    _corsService.ApplyResult(corsResult, context.Response);

                    if (isPreflight)
                    {
                        context.Response.StatusCode = StatusCodes.Status204NoContent;
                        return;
                    }
                }
            }

            await _next(context);
        }

        private async Task<CorsPolicy> GetPolicy(HttpContext context, bool isPreflight)
        {
            if (isPreflight && await _allowedOriginsProvider.IsOriginAvailable(context.Request.Headers[PolicyConstants.OriginHeader]))
                return await _corsPolicyProvider.GetPolicyAsync(context, PolicyConstants.PreflightPolicy);
            else if (context.Request.Headers.ContainsKey(PolicyConstants.TenantHeader))
                return await _corsPolicyProvider.GetPolicyAsync(context, context.Request.Headers[PolicyConstants.TenantHeader]);
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