using IdentityIssuer.Application.Models;
using System;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Users.Commands
{
    public class UpdateUserDisplayNameCommand : Request
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