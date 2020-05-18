using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Auth.Commands
{
    public class LoginUserWithCredentialsCommand: Request<AuthorizationData>
    {
        public LoginUserWithCredentialsCommand(
            string email, 
            string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }
}