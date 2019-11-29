using IdentityIssuer.Application.Requests;
using IdentityIssuer.Application.Users.Models;

namespace IdentityIssuer.Application.Users.Commands
{
    public class AuthUserCommandResult : RequestResultBase
    {
        public AuthUserCommandResult()
            : base()
        {
        }

        public AuthUserCommandResult(string error)
            : base(error)
        {
        }

        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}