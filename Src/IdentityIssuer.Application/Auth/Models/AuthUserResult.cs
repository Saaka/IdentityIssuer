using IdentityIssuer.Application.Users.Models;

namespace IdentityIssuer.Application.Auth.Models
{
    public class AuthUserResult
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}