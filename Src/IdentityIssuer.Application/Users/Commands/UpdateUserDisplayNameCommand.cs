using System;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Users.Commands
{
    public class UpdateUserDisplayNameCommand : Request
    {
        public UpdateUserDisplayNameCommand(
            string name,
            Guid userGuid)
        {
            Name = name;
            UserGuid = userGuid;
        }

        public string Name { get; }
        public Guid UserGuid { get; }
    }
}