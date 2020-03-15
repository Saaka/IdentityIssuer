using IdentityIssuer.Application.Models;
using System;

namespace IdentityIssuer.Application.Users.Commands
{
    public class UpdateUserDisplayNameCommand : CommandBase
    {
        public UpdateUserDisplayNameCommand(
            string name,
            Guid userGuid,
            UserContextData tenant)
        {
            Name = name;
            UserGuid = userGuid;
            User = tenant;
        }

        public string Name { get; }
        public Guid UserGuid { get; }
        public UserContextData User { get; }
    }
}