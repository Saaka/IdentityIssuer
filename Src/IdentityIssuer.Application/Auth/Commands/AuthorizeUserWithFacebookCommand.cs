using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Auth.Commands
{
    public class AuthorizeUserWithFacebookCommand : Request<AuthorizationData>
    {
        public AuthorizeUserWithFacebookCommand(
            string token)
        {
            Token = token;
        }

        public string Token { get; }
    }
}