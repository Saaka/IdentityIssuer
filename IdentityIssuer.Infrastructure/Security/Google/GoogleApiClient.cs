using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Configuration;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Common.Exceptions;
using IdentityIssuer.Infrastructure.Http;
using RestSharp;

namespace IdentityIssuer.Infrastructure.Security.Google
{
    public class GoogleApiClient : IGoogleApiClient
    {
        private readonly IRestSharpClientFactory clientFactory;
        private readonly IGoogleConfiguration googleConfiguration;
        private readonly IMapper mapper;
        private const string TokenInfoAddress = "tokeninfo?id_token=";

        public GoogleApiClient(
            IRestSharpClientFactory restSharpClientFactory,
            IGoogleConfiguration googleConfiguration,
            IMapper mapper)
        {
            this.clientFactory = restSharpClientFactory;
            this.googleConfiguration = googleConfiguration;
            this.mapper = mapper;
        }

        public async Task<TokenInfo> GetTokenInfoAsync(string token)
        {
            var client = clientFactory.CreateClient(googleConfiguration.ValidationEndpoint);
            var request = clientFactory.CreateRequest($"{TokenInfoAddress}{token}", Method.GET);

            var response = await client.ExecuteTaskAsync<GoogleTokenInfo>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new ProviderCommunicationException(response.ErrorMessage ?? response.StatusCode.ToString());

            return mapper.Map<TokenInfo>(response.Data);
        }
    }
}