using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Tenants.Commands.Models
{
    public class CreateTenantResult : CommandResult
    {
        public CreateTenantResult(bool isSuccessful, Tenant tenant = null)
            : base(isSuccessful)
        {
            Tenant = tenant;
        }

        public Tenant Tenant { get; }
    }
}