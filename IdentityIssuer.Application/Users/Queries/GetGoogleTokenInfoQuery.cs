using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Users.Queries
{
    public class GetGoogleTokenInfoQuery: QueryBase<TokenInfo>
    {
        public string Token { get; set; }
        public TenantContextData Tenant { get; set; }
    }
}