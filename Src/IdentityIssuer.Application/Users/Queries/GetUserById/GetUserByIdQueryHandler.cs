using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Users.Models;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;
using MediatR;

namespace IdentityIssuer.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler: IRequestHandler<GetUserByIdQuery, UserDto>
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
        
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _usersProvider.GetUser(request.UserId, request.Tenant.TenantId);
            if (user == null)
                throw new DomainException(ExceptionCode.UserNotFound, 
                    new { userGuid = request.UserGuid });
            
            return _mapper.Map<UserDto>(user);
        }
    }
}