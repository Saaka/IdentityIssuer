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
        private readonly CorsPolicy _policy;
        private readonly string _corsPolicyName;

        public TenantCorsMiddleware(
            RequestDelegate next,
            ICorsService corsService,
            ICorsPolicyProvider policyProvider)
            : this(next, corsService, policyProvider, policyName: null) { }

        public TenantCorsMiddleware(
            RequestDelegate next,
            ICorsService corsService,
            ICorsPolicyProvider policyProvider,
            string policyName)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _corsService = corsService ?? throw new ArgumentNullException(nameof(corsService));
            _corsPolicyProvider = policyProvider ?? throw new ArgumentNullException(nameof(policyProvider));
            _corsPolicyName = policyName;
        }

        public TenantCorsMiddleware(
           RequestDelegate next,
           ICorsService corsService,
           CorsPolicy policy)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _corsService = corsService ?? throw new ArgumentNullException(nameof(corsService));
            _policy = policy ?? throw new ArgumentNullException(nameof(policy));
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey(CorsConstants.Origin))
            {
                CorsPolicy corsPolicy = null;
                var accessControlRequestMethod = context.Request.Headers[CorsConstants.AccessControlRequestMethod];

                if (context.Request.Headers.ContainsKey(PolicyConstants.TenantHeader))
                {
                    corsPolicy = _policy ?? await _corsPolicyProvider?.GetPolicyAsync(context, context.Request.Headers[PolicyConstants.TenantHeader]);
                }

                if (corsPolicy != null)
                {
                    var corsResult = _corsService.EvaluatePolicy(context, corsPolicy);
                    _corsService.ApplyResult(corsResult, context.Response);

                    if (IsPreflight(context, accessControlRequestMethod))
                    {
                        // Since there is a policy which was identified,
                        // always respond to preflight requests.
                        context.Response.StatusCode = StatusCodes.Status204NoContent;
                        return;
                    }
                }
            }

            await _next(context);
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