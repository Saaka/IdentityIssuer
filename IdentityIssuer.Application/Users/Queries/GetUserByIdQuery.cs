using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Users.Models;

namespace IdentityIssuer.Application.Users.Queries
{
    public class GetUserByIdQuery: QueryBase<UserDto>
    {
        public GetUserByIdQuery(
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