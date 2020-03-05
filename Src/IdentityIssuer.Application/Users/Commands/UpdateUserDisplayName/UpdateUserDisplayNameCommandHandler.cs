using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Application.Users.Repositories;
using MediatR;

namespace IdentityIssuer.Application.Users.Commands.UpdateUserDisplayName
{
    public class UpdateUserDisplayNameCommandHandler : AsyncRequestHandler<UpdateUserDisplayNameCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserDisplayNameCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected override async Task Handle(UpdateUserDisplayNameCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.UpdateUserDisplayName(request.UserGuid, request.Name);
        }
    }
}