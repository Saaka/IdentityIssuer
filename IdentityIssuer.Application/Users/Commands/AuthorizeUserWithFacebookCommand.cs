using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Users.Models;

namespace IdentityIssuer.Application.Users.Commands
{
    public class AuthorizeUserWithFacebookCommand : CommandBase<AuthUserResult>
    {
        public AuthorizeUserWithFacebookCommand(
            string token, 
            TenantContextData tenant)
        {
            Token = token;
            Tenant = tenant;
        }

        public string Token { get; }
        public TenantContextData Tenant { get; }
    }
}