using System;

namespace IdentityIssuer.Application.Models.RequestContext
{
    public class UserContext
    {
        public int UserId { get; }
        public Guid UserGuid { get; }
        public bool IsAdmin { get; }
    }
}