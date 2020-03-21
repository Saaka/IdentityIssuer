using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands
{
    public class CreateTenantCommand : Request<TenantDto>
    {
        public string Name { get; }
        public string Code { get; }
        public string AllowedOrigin { get; }
        public AdminContextData AdminContextData { get; }

        public CreateTenantCommand(
            string name,
            string code,
            string allowedOrigin,
            AdminContextData adminContextData)
        {
            Name = name;
            Code = code;
            AllowedOrigin = allowedOrigin;
            AdminContextData = adminContextData;
        }
    }
}