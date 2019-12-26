using System.Threading.Tasks;
using IdentityIssuer.Application.Users.Commands;
using IdentityIssuer.Application.Users.Queries;
using IdentityIssuer.WebAPI.Controllers.Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityIssuer.WebAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    public class AuthController : BaseApiController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserWithCredentialsRequest request)
        {
            var guid = GuidProvider.GetNormalizedGuid();
            await Mediator.Send(new RegisterUserWithCredentialsCommand
            {
                UserGuid = guid,
                Email = request.Email,
                Password = request.Password,
                DisplayName = request.DisplayName,
                Tenant = await GetTenant()
            });

            return Ok(guid);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserWithCredentialsRequest request)
        {
            var result = await Mediator.Send(new GetUserByCredentialsQuery
            {
                Email = request.Email,
                Password = request.Password,
                Tenant = await GetTenant()
            });

            return Ok(result);
        }
    }
}