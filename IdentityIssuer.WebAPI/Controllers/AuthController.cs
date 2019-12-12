using System.Threading.Tasks;
using IdentityIssuer.Application.Users.Commands;
using Microsoft.AspNetCore.Mvc;

namespace IdentityIssuer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseApiController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserWithCredentialsCommand request)
        {
            var result = await Mediator.Send(request);

            return GetRequestResult(result);
        }
    }
}