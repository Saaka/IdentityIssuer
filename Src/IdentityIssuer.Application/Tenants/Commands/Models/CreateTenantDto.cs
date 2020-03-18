namespace IdentityIssuer.Application.Tenants.Commands.Models
{
    public class CreateTenantDto
    {
        public CreateTenantDto(string name, string code, string allowedOrigin)
        {
            Name = name;
            Code = code;
            AllowedOrigin = allowedOrigin;
        }
        public string Name { get;  }
        public string Code { get;  }
        public string AllowedOrigin { get; }
    }
}