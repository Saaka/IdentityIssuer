namespace IdentityIssuer.Application.Models
{
    public class TokenInfo
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string ImageUrl { get; set; }
        public string ClientId { get; set; }
        public string ExternalUserId { get; set; }
    }
}