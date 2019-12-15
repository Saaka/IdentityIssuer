using System;
using MediatR;

namespace IdentityIssuer.Application.Users.Commands
{
    public class RegisterUserWithCredentialsCommand : IRequest
    {
        public Guid Guid { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public int TenantId { get; set; }
    }
}