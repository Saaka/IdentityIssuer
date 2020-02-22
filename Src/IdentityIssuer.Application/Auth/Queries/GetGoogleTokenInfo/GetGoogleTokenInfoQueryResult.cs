using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Auth.Queries.GetGoogleTokenInfo
{
    public class GetGoogleTokenInfoQueryResult
    {
        public TokenInfo TokenInfo { get; set; }
        public bool IsGoogleUserRegistered { get; set; }
        public bool IsEmailRegistered { get; set; }
    }
}