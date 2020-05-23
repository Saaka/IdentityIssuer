using System.Threading.Tasks;
using IdentityIssuer.Application.Auth.Commands;
using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Users.Models;
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
        public async Task<ActionResult<UserDto>> Register(RegisterUserWithCredentialsRequest request)
        {
            var guid = GuidProvider.GetGuid();
            var result = await Mediator.Send(new RegisterUserWithCredentialsCommand(
                userGuid: guid,
                email: request.Email,
                displayName: request.DisplayName,
                password: request.Password
            ).WithRequestContext(await GetRequestContext()));

            return GetResponse(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthorizationData>> Login(LoginUserWithCredentialsRequest request)
        {
            var result = await Mediator.Send(new LoginUserWithCredentialsCommand(
                email: request.Email,
                password: request.Password
            ).WithRequestContext(await GetRequestContext()));

            return GetResponse(result);
        }

        [HttpPost("google")]
        public async Task<ActionResult<AuthorizationData>> AuthorizeWithGoogle(AuthorizeUserWithGoogleRequest request)
        {
            var tokenResult = await Mediator.Send(new AuthorizeUserWithGoogleCommand(
                token: request.GoogleToken
            ).WithRequestContext(await GetRequestContext()));

            return GetResponse(tokenResult);
        }

        [HttpPost("facebook")]
        public async Task<ActionResult<AuthorizationData>> AuthorizeWithFacebook(
            AuthorizeUserWithFacebookRequest request)
        {
            var tokenResult = await Mediator.Send(new AuthorizeUserWithFacebookCommand(
                token: request.FacebookToken
            ).WithRequestContext(await GetRequestContext()));

            return GetResponse(tokenResult);
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult<UserDto>> GetUserData()
        {
            var context = await GetRequestContext();
            var result = await Mediator.Send(new GetUserByIdQuery(
                context.User.UserId,
                context.User.UserGuid
            ).WithRequestContext(context));

            return GetResponse(result);
        }

        [HttpGet("confirm/{confirmationToken}")]
        public async Task<IActionResult> Confirm(string confirmationToken)
        {
            return Redirect("https://google.com");
        }
    }
}