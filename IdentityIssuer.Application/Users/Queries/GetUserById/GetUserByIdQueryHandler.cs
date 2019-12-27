using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Users.Models;
using IdentityIssuer.Common.Exceptions;
using MediatR;

namespace IdentityIssuer.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler: IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUsersProvider usersProvider;
        private readonly IMapper mapper;

        public GetUserByIdQueryHandler(
            IUsersProvider usersProvider, 
            IMapper mapper)
        {
            this.usersProvider = usersProvider;
            this.mapper = mapper;
        }
        
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await usersProvider.GetUser(request.UserId, request.Tenant.TenantId);
            if (user == null)
                throw new UserNotFoundException(request.UserGuid);
            
            return mapper.Map<UserDto>(user);
        }
    }
}