using System;

namespace IdentityIssuer.Application.Models.RequestContext
{
    public class UserContext
    {
        public UserContext(
            int userId,
            Guid userGuid,
            bool isAdmin)
        {
            UserId = userId;
            UserGuid = userGuid;
            IsAdmin = isAdmin;
        }

        public int UserId { get; }
        public Guid UserGuid { get; }
        public bool IsAdmin { get; }
    }
}