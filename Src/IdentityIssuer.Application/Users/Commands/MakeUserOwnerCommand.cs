using System;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Users.Commands
{
    public class MakeUserOwnerCommand : Request<Guid>
    {
        public MakeUserOwnerCommand(Guid userGuid)
        {
            UserGuid = userGuid;
        }

        public Guid UserGuid { get; }
    }
}