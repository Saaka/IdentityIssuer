using System;
using IdentityIssuer.Common.Events;

namespace IdentityIssuer.Application.Auth.Events
{
    public class UserRegisteredWithCredentials : Event
    {
        public UserRegisteredWithCredentials(Guid userGuid)
        {
            UserGuid = userGuid;
        }
        public Guid UserGuid { get; }
    }
}