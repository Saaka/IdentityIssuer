using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Auth.Commands
{
    public class LoginUserWithCredentialsCommand: Request<AuthorizationData>
    {
        public LoginUserWithCredentialsCommand(
            string email, 
            string password, 
            TenantContextData tenant)
        {
            Email = email;
            Password = password;
            Tenant = tenant;
        }

        public string Email { get; }
        public string Password { get; }
        public TenantContextData Tenant { get; }
    }
}