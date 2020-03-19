using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Users.Models;
using System;
using IdentityIssuer.Application.Requests;

namespace IdentityIssuer.Application.Users.Queries
{
    public class GetUserByIdQuery: Request<UserDto>
    {
        public GetUserByIdQuery(
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