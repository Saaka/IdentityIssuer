using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Users.Models;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : RequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUsersProvider _usersProvider;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(
            IUsersProvider usersProvider,
            IMapper mapper)
        {
            _usersProvider = usersProvider;
            _mapper = mapper;
        }

        public override async Task<RequestResult<UserDto>> Handle(GetUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _usersProvider.GetUser(request.UserId, request.Tenant.TenantId);
            if (user == null)
                return RequestResult<UserDto>
                    .Failure(ErrorCode.UserNotFound, new {userGuid = request.UserGuid});

            return RequestResult<UserDto>
                .Success(_mapper.Map<UserDto>(user));
        }
    }
}