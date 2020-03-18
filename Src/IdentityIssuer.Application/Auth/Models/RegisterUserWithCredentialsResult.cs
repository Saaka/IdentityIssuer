using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Users.Models;

namespace IdentityIssuer.Application.Auth.Models
{
    public class RegisterUserWithCredentialsResult : CommandResult
    {
        public RegisterUserWithCredentialsResult(bool isSuccessful, TenantUser user)
            : base(isSuccessful)
        {
            User = user;
        }

        public TenantUser User { get; }
    }
}