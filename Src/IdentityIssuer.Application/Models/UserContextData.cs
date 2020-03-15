using System;

namespace IdentityIssuer.Application.Models
{
    public class UserContextData
    {
        public UserContextData(
            int userId,
            Guid userGuid,
            TenantContextData tenant)
        {
            UserId = userId;
            UserGuid = userGuid;
            Tenant = tenant;
        }

        public int UserId { get; }
        public Guid UserGuid { get; }
        public TenantContextData Tenant { get; }
    }
}