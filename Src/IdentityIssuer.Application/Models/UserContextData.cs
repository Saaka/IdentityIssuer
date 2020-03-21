using System;

namespace IdentityIssuer.Application.Models
{
    public class UserContextData
    {
        public UserContextData(
            int userId,
            Guid userGuid,
            bool isAdmin,
            TenantContextData tenant)
        {
            UserId = userId;
            UserGuid = userGuid;
            IsAdmin = isAdmin;
            Tenant = tenant;
        }

        public int UserId { get; }
        public Guid UserGuid { get; }
        public bool IsAdmin { get; }
        public TenantContextData Tenant { get; }
    }
}