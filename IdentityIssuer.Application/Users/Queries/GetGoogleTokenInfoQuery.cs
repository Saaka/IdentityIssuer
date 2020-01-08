using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Users.Queries.GetGoogleTokenInfo;

namespace IdentityIssuer.Application.Users.Queries
{
    public class GetGoogleTokenInfoQuery: QueryBase<GetGoogleTokenInfoQueryResult>
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