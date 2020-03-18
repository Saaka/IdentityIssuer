using IdentityIssuer.Application.Tenants.Models;

namespace IdentityIssuer.Application.Tenants.Commands
{
    public class CreateTenantCommand : CommandBase<CreateTenantResult>
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