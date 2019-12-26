namespace IdentityIssuer.Application.Models
{
    public class UserContextData
    {
        public UserContextData(int userId, string userGuid, int tenantId, string tenantCode)
        {
            UserId = userId;
            UserGuid = userGuid;
            TenantId = tenantId;
            TenantCode = tenantCode;
        }
        public int UserId { get;  }
        public string UserGuid { get; }
        public int TenantId { get;  }
        public string TenantCode { get; }
    }
}