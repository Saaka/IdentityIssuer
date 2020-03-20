using System.Threading.Tasks;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Common.Requests;
using IdentityIssuer.WebAPI.Models;
using IdentityIssuer.WebAPI.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityIssuer.WebAPI.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        private IGuid _guid;
        private IContextDataProvider _contextDataProvider;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected IGuid GuidProvider => _guid ??= HttpContext.RequestServices.GetService<IGuid>();

        protected IContextDataProvider ContextDataProvider =>
            _contextDataProvider ??= HttpContext.RequestServices.GetService<IContextDataProvider>();

        protected async Task<TenantContextData> GetTenant()
        {
            return await ContextDataProvider.GetTenant(HttpContext);
        }

        protected async Task<UserContextData> GetUser()
        {
            return await ContextDataProvider.GetUser(HttpContext);
        }

        protected ActionResult<TResponseType> GetResponse<TResponseType>(RequestResult<TResponseType> result)
        {
            if (result.IsSuccess)
                return Ok(result.Data);
            else
                return BadRequest(new ErrorResponse(result.Error, result.ErrorDetails.ToString()));
        }
    }
}