using System;
using System.Threading.Tasks;
using IdentityIssuer.Application.Users.Commands;
using IdentityIssuer.WebAPI.Controllers.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityIssuer.WebAPI.Controllers.Users
{
    [Route("api/[controller]")]
    public class UsersController : BaseApiController
    {
        [Authorize]
        [HttpPost("name")]
        public async Task<ActionResult<Guid>> UpdateUserDisplayName(UpdateUserDisplayNameModel model)
        {
            var requestContext = await GetRequestContext();
            var result = await Mediator.Send(
                new UpdateUserDisplayNameCommand(model.Name, model.UserGuid)
                    .WithRequestContext(requestContext));

            return GetResponse(result);
        }
    }
}