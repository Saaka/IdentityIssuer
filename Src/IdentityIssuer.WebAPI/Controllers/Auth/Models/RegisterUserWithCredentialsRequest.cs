namespace IdentityIssuer.WebAPI.Controllers.Auth.Models
{
    public class RegisterUserWithCredentialsRequest
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
    }
}