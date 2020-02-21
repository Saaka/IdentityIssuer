using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Users.Models;

namespace IdentityIssuer.Application.Users.Queries
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