using Newtonsoft.Json;

namespace IdentityIssuer.Infrastructure.Security.Facebook.Models
{
    public class FbUserDataResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("picture")]
        public FbPictureDataContainer Picture { get; set; }
    }

    public class FbPictureDataContainer
    {
        [JsonProperty("data")]
        public FbPictureData Data { get; set; }
    }

    public class FbPictureData
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}