using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Auth.Commands
{
    public class AuthorizeUserWithGoogleCommand : Request<AuthorizationData>
    {
        public AuthorizeUserWithGoogleCommand(
            string token)
        {
            Token = token;
        }

        public string Token { get; }
        public TenantContextData Tenant { get; }
    }
}