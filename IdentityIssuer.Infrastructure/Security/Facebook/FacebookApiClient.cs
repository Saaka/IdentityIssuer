using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Configuration;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;
using IdentityIssuer.Infrastructure.Http;
using IdentityIssuer.Infrastructure.Security.Facebook.Models;
using RestSharp;

namespace IdentityIssuer.Infrastructure.Security.Facebook
{
    public class FacebookApiClient : IFacebookApiClient
    {
        private readonly IRestSharpClientFactory clientFactory;
        private readonly IFacebookConfiguration facebookConfiguration;
        private readonly IMapper mapper;

        public FacebookApiClient(
            IRestSharpClientFactory clientFactory,
            IFacebookConfiguration facebookConfiguration,
            IMapper mapper)
        {
            this.clientFactory = clientFactory;
            this.facebookConfiguration = facebookConfiguration;
            this.mapper = mapper;
        }

        public async Task<TokenInfo> GetTokenInfoAsync(string token, string appId, string appSecret)
        {
            var client = clientFactory.CreateClient(facebookConfiguration.FacebookValidationEndpoint);
            var appAccessToken = await GetAppAccessToken(client, appId, appSecret);

            var verifyTokenResult = await VerifyToken(client, token, appAccessToken);
            if (!verifyTokenResult.Scopes.Contains("email"))
                throw new DomainException(ExceptionCode.FacebookTokenEmailPermissionRequired);
            var userData = await GetUserData(client, token, verifyTokenResult.UserId);

            return new TokenInfo
            {
                ClientId = verifyTokenResult.AppId,
                DisplayName = userData.Name,
                Email = userData.Email,
                ExternalUserId = userData.Id,
                ImageUrl = userData.Picture.Data.Url
            };
        }

        private async Task<FbUserDataResponse> GetUserData(IRestClient client, string token, string userId)
        {
            var request = clientFactory.CreateRequest(
                $"{userId}" +
                $"?fields=id,name,email,picture" +
                $"&access_token={token}", Method.GET);

            var response = await client.ExecuteTaskAsync<FbUserDataResponse>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new ProviderCommunicationException(response.ErrorMessage ?? response.StatusCode.ToString());

            return response.Data;
        }

        private async Task<FbVerifyTokenResultData> VerifyToken(IRestClient client, string token, string appAccessToken)
        {
            var request = clientFactory.CreateRequest(
                $"debug_token?" +
                $"input_token={token}" +
                $"&access_token={appAccessToken}", Method.GET);

            var response = await client.ExecuteTaskAsync<FbVerifyTokenResult>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new ProviderCommunicationException(response.ErrorMessage ?? response.StatusCode.ToString());

            return response.Data.Data;
        }

        private async Task<string> GetAppAccessToken(IRestClient client, string appId, string appSecret)
        {
            var request = clientFactory.CreateRequest(
                $"oauth/access_token?" +
                $"client_id={appId}" +
                $"&client_secret={appSecret}" +
                $"&grant_type=client_credentials",
                Method.GET);

            var response = await client.ExecuteTaskAsync<FbAccessTokenResponse>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new ProviderCommunicationException(response.ErrorMessage ?? response.StatusCode.ToString());

            return response.Data.AccessToken;
        }
    }
}