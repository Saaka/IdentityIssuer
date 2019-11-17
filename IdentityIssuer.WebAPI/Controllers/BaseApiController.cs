using IdentityIssuer.Application.Services;
using IdentityIssuer.WebAPI.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityIssuer.WebAPI.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator mediator;
        private IGuid guid;
        private IContextDataProvider contextDataProvider;

        protected IMediator Mediator => mediator ?? (mediator = HttpContext.RequestServices.GetService<IMediator>());
        protected IGuid GuidProvider => guid ?? (guid = HttpContext.RequestServices.GetService<IGuid>());

        protected IContextDataProvider ContextDataProvider =>
            contextDataProvider ??
            (contextDataProvider = HttpContext.RequestServices.GetService<IContextDataProvider>());
    }
}