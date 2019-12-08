using System.Threading.Tasks;
using IdentityIssuer.Application.Requests;
using IdentityIssuer.WebAPI.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityIssuer.WebAPI.Pipeline
{
    public class TenantActionFilter : IAsyncActionFilter
    {
        private readonly IContextDataProvider contextDataProvider;

        public TenantActionFilter(IContextDataProvider contextDataProvider)
        {
            this.contextDataProvider = contextDataProvider;
        }
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (!(argument is ITenantCommand command)) continue;
                
                var tenant = await contextDataProvider.GetTenant(context.HttpContext);
                command.TenantId = tenant.TenantId;
            }
            
            await next();
        }
    }
}