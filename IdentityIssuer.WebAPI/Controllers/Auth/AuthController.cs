using System.Threading.Tasks;
using IdentityIssuer.Application.Users.Commands;
using IdentityIssuer.WebAPI.Controllers.Auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdentityIssuer.WebAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    public class AuthController : BaseApiController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserWithCredentialsRequest request)
        {
            var guid = GuidProvider.GetGuid();
            await Mediator.Send(new RegisterUserWithCredentialsCommand
            {
                UserGuid = guid,
                Email = request.Email,
                Password = request.Password,
                DisplayName = request.DisplayName,
                TenantId = await GetTenantId()
            });

            return Ok(guid);
        }
    }
}