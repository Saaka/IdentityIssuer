using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Auth.Queries
{
    public class GetUserByCredentialsQuery : QueryBase<AuthUserResult>
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