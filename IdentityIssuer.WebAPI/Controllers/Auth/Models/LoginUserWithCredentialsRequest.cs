namespace IdentityIssuer.WebAPI.Controllers.Auth.Models
{
    public class LoginUserWithCredentialsRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }    
    }
}