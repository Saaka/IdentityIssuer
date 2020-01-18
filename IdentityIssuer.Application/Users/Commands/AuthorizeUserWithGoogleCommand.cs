using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Users.Models;

namespace IdentityIssuer.Application.Users.Commands
{
    public class AuthorizeUserWithGoogleCommand : CommandBase<AuthUserResult>
    {
        public AuthorizeUserWithGoogleCommand(
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