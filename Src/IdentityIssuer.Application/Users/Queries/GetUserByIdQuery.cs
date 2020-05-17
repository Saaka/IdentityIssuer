using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Users.Models;
using System;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Users.Queries
{
    public class GetUserByIdQuery: Request<UserDto>
    {
        public GetUserByIdQuery(
            int userId,
            Guid userGuid)
        {
            UserId = userId;
            UserGuid = userGuid;
        }

        public int UserId { get; }
        public Guid UserGuid { get; }
        public TenantContextData Tenant { get; }
    }
}