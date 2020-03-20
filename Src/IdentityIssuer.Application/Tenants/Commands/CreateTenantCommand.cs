using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands
{
    public class CreateTenantCommand : Request<Tenant>
    {
        protected CreateTenantCommand
            (string name, string code, string allowedOrigin)
        {
            Name = name;
            Code = code;
            AllowedOrigin = allowedOrigin;
        }

        public string Name { get; }
        public string Code { get; }
        public string AllowedOrigin { get; }
    }
}