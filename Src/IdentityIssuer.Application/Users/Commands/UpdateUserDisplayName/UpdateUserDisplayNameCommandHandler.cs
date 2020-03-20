using System;
using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Application.Users.Repositories;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Users.Commands.UpdateUserDisplayName
{
    public class UpdateUserDisplayNameCommandHandler : RequestHandler<UpdateUserDisplayNameCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserDisplayNameCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task<RequestResult<Guid>> Handle(UpdateUserDisplayNameCommand request,
            CancellationToken cancellationToken)
        {
            await _userRepository.UpdateUserDisplayName(request.UserGuid, request.Name);

            return RequestResult
                .Success(request);
        }
    }
}