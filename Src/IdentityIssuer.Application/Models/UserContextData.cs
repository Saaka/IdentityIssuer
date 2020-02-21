namespace IdentityIssuer.Application.Models
{
    public class UserContextData
    {
        public UserContextData(
            int userId,
            string userGuid,
            TenantContextData tenant)
        {
            UserId = userId;
            UserGuid = userGuid;
            Tenant = tenant;
        }

        public int UserId { get; }
        public string UserGuid { get; }
        public TenantContextData Tenant { get; }
    }
}