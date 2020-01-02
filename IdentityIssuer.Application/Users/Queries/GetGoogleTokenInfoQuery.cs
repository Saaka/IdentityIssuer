using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Users.Queries
{
    public class GetGoogleTokenInfoQuery: QueryBase<TokenInfo>
    {
        public GetGoogleTokenInfoQuery(string token, TenantContextData tenant)
        {
            Token = token;
            Tenant = tenant;
        }

        public string Token { get; }
        public TenantContextData Tenant { get; }
    }
}