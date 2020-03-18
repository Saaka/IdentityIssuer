using IdentityIssuer.Application.Tenants.Models;

namespace IdentityIssuer.Application.Tenants.Commands
{
    public class CreateTenantCommand : CommandBase<CreateTenantResult>
    {
        protected CreateTenantCommand
            (string name, string code, string adminEmail, string adminPassword, string allowedOrigin)
        {
            Name = name;
            Code = code;
            AdminEmail = adminEmail;
            AdminPassword = adminPassword;
            AllowedOrigin = allowedOrigin;
        }

        public string Name { get; }
        public string Code { get; }
        public string AdminEmail { get; }
        public string AdminPassword { get; }
        public string AllowedOrigin { get; }
    }
}