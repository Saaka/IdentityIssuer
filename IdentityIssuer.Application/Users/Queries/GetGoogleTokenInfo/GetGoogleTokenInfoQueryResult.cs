using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Users.Queries.GetGoogleTokenInfo
{
    public class GetGoogleTokenInfoQueryResult
    {
        public TokenInfo TokenInfo { get; set; }
        public bool IsGoogleUserRegistered { get; set; }
    }
}