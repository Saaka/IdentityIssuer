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
        [HttpPost("user/name")]
        public async Task<IActionResult> UpdateUserDisplayName(UpdateUserDisplayNameModel model)
        {
            var user = await GetUser();
            await Mediator.Send(new UpdateUserDisplayNameCommand(model.Name, user));
            
            return Ok();
        }
    }
}