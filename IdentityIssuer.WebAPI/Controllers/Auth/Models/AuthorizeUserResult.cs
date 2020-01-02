namespace IdentityIssuer.WebAPI.Controllers.Auth.Models
{
    public class AuthorizeUserResult
    {
        public string UserGuid { get; set; }
        public bool IsNewUser { get; set; }
    }
}