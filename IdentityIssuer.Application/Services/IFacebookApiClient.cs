using System.Threading.Tasks;
using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Services
{
    public interface IFacebookApiClient
    {
        Task<TokenInfo> GetTokenInfoAsync(string token, string appId, string appSecret);
    }
}