namespace IdentityIssuer.Application.Users.Models
{
    public class AuthUserResult
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}