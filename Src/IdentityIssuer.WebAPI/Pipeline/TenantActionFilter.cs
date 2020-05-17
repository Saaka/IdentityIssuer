using System.Threading.Tasks;
using IdentityIssuer.WebAPI.Models;
using IdentityIssuer.WebAPI.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityIssuer.WebAPI.Pipeline
{
    public class TenantActionFilter : IAsyncActionFilter
    {
        private readonly IContextDataProvider _contextDataProvider;

        public TenantActionFilter(IContextDataProvider contextDataProvider)
        {
            _contextDataProvider = contextDataProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (!(argument is ITenantRequest request)) continue;

                var requestContext = await _contextDataProvider.GetRequestContext(context.HttpContext);
                if (requestContext.IsTenantContext)
                    request.TenantId = requestContext.Tenant.TenantId;
            }

            await next();
        }
    }
}