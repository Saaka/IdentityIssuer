using System.Threading.Tasks;
using IdentityIssuer.Application.Auth.Commands;
using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Auth.Queries;
using IdentityIssuer.Application.Models;
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
                password: request.Password,
                tenant: await GetTenantAsync()
            ));

            return GetResponse(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthorizationData>> Login(LoginUserWithCredentialsRequest request)
        {
            var result = await Mediator.Send(new GetUserByCredentialsQuery(
                email: request.Email,
                password: request.Password,
                tenant: await GetTenantAsync()
            ));

            return GetResponse(result);
        }

        [HttpPost("google")]
        public async Task<ActionResult<AuthorizationData>> AuthorizeWithGoogle(AuthorizeUserWithGoogleRequest request)
        {
            var tenant = await GetTenantAsync();
            var tokenResult = await Mediator.Send(new AuthorizeUserWithGoogleCommand(
                token: request.GoogleToken,
                tenant: tenant));
            
            return GetResponse(tokenResult);
        }

        [HttpPost("facebook")]
        public async Task<ActionResult<AuthorizationData>> AuthorizeWithFacebook(AuthorizeUserWithFacebookRequest request)
        {
            var tenant = await GetTenantAsync();
            var tokenResult = await Mediator.Send(new AuthorizeUserWithFacebookCommand(
                token: request.FacebookToken,
                tenant: tenant));
            
            return GetResponse(tokenResult);
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult<UserDto>> GetUserData()
        {
            var user = await GetUserAsync();
            var result = await Mediator.Send(new GetUserByIdQuery(
                user.UserId, 
                user.UserGuid,
                user.Tenant));

            return GetResponse(result);
        }
    }
}