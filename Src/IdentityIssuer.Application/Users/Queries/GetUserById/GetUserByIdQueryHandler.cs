using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Users.Models;
using IdentityIssuer.Application.Users.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : RequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public override async Task<RequestResult<UserDto>> Handle(GetUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.UserId, request.RequestContext.Tenant.TenantId);
            if (user == null)
                return RequestResult<UserDto>
                    .Failure(ErrorCode.UserNotFound, new {userGuid = request.UserGuid});

            return RequestResult<UserDto>
                .Success(_mapper.Map<UserDto>(user));
        }
    }
}