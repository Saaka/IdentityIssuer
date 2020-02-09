using Newtonsoft.Json;

namespace IdentityIssuer.Infrastructure.Security.Facebook.Models
{
    public class FbVerifyTokenResult
    {
        [JsonProperty("data")]
        public FbVerifyTokenResultData Data { get; set; }
    }
    public class FbVerifyTokenResultData
    {
        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }
        [JsonProperty("scopes")]
        public string[] Scopes { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("app_id")]
        public string AppId { get; set; }
    }
}