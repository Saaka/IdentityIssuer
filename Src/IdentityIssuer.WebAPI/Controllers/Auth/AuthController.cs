using System;
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
            await Mediator.Send(new RegisterUserWithCredentialsCommand(
                userGuid: guid,
                email: request.Email,
                displayName: request.DisplayName,
                password: request.Password,
                tenant: await GetTenant()
            ));

            return Ok(guid);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserWithCredentialsRequest request)
        {
            var result = await Mediator.Send(new GetUserByCredentialsQuery(
                email: request.Email,
                password: request.Password,
                tenant: await GetTenant()
            ));

            return Ok(result);
        }

        [HttpPost("google")]
        public async Task<IActionResult> AuthorizeWithGoogle(AuthorizeUserWithGoogleRequest request)
        {
            var tenant = await GetTenant();
            var tokenResult = await Mediator.Send(new AuthorizeUserWithGoogleCommand(
                token: request.GoogleToken,
                tenant: tenant));
            
            return Ok(tokenResult);
        }

        [HttpPost("facebook")]
        public async Task<IActionResult> AuthorizeWithFacebook(AuthorizeUserWithFacebookRequest request)
        {
            var tenant = await GetTenant();
            var tokenResult = await Mediator.Send(new AuthorizeUserWithFacebookCommand(
                token: request.FacebookToken,
                tenant: tenant));
            
            return Ok(tokenResult);
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetUserData()
        {
            var user = await GetUser();
            var result = await Mediator.Send(new GetUserByIdQuery(
                user.UserId, 
                user.UserGuid,
                user.Tenant));

            return Ok(result);
        }
    }
}