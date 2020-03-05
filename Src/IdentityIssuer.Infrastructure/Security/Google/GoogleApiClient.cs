using System.Net;
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
        private readonly IRestSharpClientFactory _clientFactory;
        private readonly IGoogleConfiguration _googleConfiguration;
        private readonly IMapper _mapper;
        private const string TokenInfoAddress = "tokeninfo?id_token=";
        private const string InvalidValueContentString = "Invalid Value";

        public GoogleApiClient(
            IRestSharpClientFactory restSharpClientFactory,
            IGoogleConfiguration googleConfiguration,
            IMapper mapper)
        {
            _clientFactory = restSharpClientFactory;
            _googleConfiguration = googleConfiguration;
            _mapper = mapper;
        }

        public async Task<TokenInfo> GetTokenInfoAsync(string token)
        {
            var client = _clientFactory.CreateClient(_googleConfiguration.GoogleValidationEndpoint);
            var request = _clientFactory.CreateRequest($"{TokenInfoAddress}{token}", Method.GET);

            var response = await client.ExecuteTaskAsync<GoogleTokenInfo>(request);
            return response.StatusCode == HttpStatusCode.OK
                ? _mapper.Map<TokenInfo>(response.Data)
                : GetInvalidResult(response);
        }

        private TokenInfo GetInvalidResult(IRestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest &&
                response.Content.Contains(InvalidValueContentString))
                return null;
            else
                throw new ProviderCommunicationException(response.ErrorMessage ?? response.StatusCode.ToString());
        }
    }
}