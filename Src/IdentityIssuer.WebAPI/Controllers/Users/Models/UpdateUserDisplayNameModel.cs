using System;

namespace IdentityIssuer.WebAPI.Controllers.Users.Models
{
    public class UpdateUserDisplayNameModel
    {
        public string Name { get; set; }
        public Guid UserGuid { get; set; }
    }
}