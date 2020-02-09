using Newtonsoft.Json;

namespace IdentityIssuer.Infrastructure.Security.Facebook.Models
{
    public class FbAccessTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}