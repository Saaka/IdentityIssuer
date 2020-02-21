using System.Threading.Tasks;
using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Services
{
    public interface IGoogleApiClient
    {
        Task<TokenInfo> GetTokenInfoAsync(string token);
    }
}