using System;
using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Users.Commands
{
    public class RegisterUserWithCredentialsCommand : CommandBase
    {
        public string UserGuid { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public TenantContextData Tenant { get; set; }
    }
}