using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Users.Models;

namespace IdentityIssuer.Application.Users.Queries
{
    public class GetUserByCredentialsQuery : QueryBase<AuthUserResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public TenantContextData Tenant { get; set; }
    }
}