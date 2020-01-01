using Newtonsoft.Json;

namespace IdentityIssuer.Infrastructure.Security.Google
{
    public class GoogleTokenInfo
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("name")]
        public string DisplayName { get; set; }
        [JsonProperty("picture")]
        public string ImageUrl { get; set; }
        [JsonProperty("aud")]
        public string ClientId { get; set; }
        [JsonProperty("sub")]
        public string GoogleUserId { get; set; }
    }
}