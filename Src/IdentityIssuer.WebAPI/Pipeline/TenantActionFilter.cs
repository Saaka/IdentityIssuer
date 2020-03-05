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
                
                var tenant = await _contextDataProvider.GetTenant(context.HttpContext);
                request.TenantId = tenant.TenantId;
            }
            
            await next();
        }
    }
}