using IdentityIssuer.Application.Auth.Queries.GetGoogleTokenInfo;
using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Auth.Queries
{
    public class GetGoogleTokenInfoQuery : QueryBase<GetGoogleTokenInfoQueryResult>
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