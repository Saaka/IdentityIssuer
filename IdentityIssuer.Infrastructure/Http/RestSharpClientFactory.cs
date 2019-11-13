using RestSharp;

namespace IdentityIssuer.Infrastructure.Http
{
    public interface IRestSharpClientFactory
    {
        IRestClient CreateClient(string url);
        IRestRequest CreateRequest(string resource, Method method);

    }

    public class RestSharpClientFactory : IRestSharpClientFactory
    {
        public IRestClient CreateClient(string url)
        {
            var client = new RestClient(url);
            client.UseSerializer<JsonNetSerializer>();

            return client;
        }

        public IRestRequest CreateRequest(string resource, Method method)
        {
            return new RestRequest(resource, method);
        }
    }
}