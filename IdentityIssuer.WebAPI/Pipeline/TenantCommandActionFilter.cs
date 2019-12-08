using System.Threading.Tasks;
using IdentityIssuer.Application.Requests;
using IdentityIssuer.WebAPI.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityIssuer.WebAPI.Pipeline
{
    public class TenantCommandActionFilter : IAsyncActionFilter
    {
        private readonly IContextDataProvider contextDataProvider;

        public TenantCommandActionFilter(IContextDataProvider contextDataProvider)
        {
            this.contextDataProvider = contextDataProvider;
        }
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.ContainsKey("request") && context.ActionArguments["request"] is ITenantCommand command)
            {
                var tenant = await contextDataProvider.GetTenant(context.HttpContext);
                command.TenantId = tenant.TenantId;
            }
            
            await next();
        }
    }
}