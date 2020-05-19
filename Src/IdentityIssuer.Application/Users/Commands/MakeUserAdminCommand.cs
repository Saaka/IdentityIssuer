using System;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Users.Commands
{
    public class MakeUserAdminCommand : Request<Guid>
    {
        public MakeUserAdminCommand(Guid userGuid)
        {
            UserGuid = userGuid;
        }

        public Guid UserGuid { get; }
    }
}