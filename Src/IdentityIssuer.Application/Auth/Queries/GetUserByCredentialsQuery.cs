using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Auth.Queries
{
    public class GetUserByCredentialsQuery : Request<AuthorizationData>
    {
        public GetUserByCredentialsQuery(
            string email, 
            string password, 
            TenantContextData tenant)
        {
            Email = email;
            Password = password;
            Tenant = tenant;
        }

        public string Email { get; }
        public string Password { get; }
        public TenantContextData Tenant { get; }
    }
}