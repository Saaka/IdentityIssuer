using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Auth.Commands
{
    public class AuthorizeUserWithFacebookCommand : CommandBase<AuthorizationData>
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