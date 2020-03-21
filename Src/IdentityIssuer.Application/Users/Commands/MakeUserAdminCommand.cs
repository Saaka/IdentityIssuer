using System;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Users.Commands
{
    public class MakeUserAdminCommand : Request<Guid>
    {
        public MakeUserAdminCommand(Guid userGuid, AdminContextData adminContextData)
        {
            UserGuid = userGuid;
            AdminContextData = adminContextData;
        }

        public Guid UserGuid { get; }
        public AdminContextData AdminContextData { get; }
    }
}