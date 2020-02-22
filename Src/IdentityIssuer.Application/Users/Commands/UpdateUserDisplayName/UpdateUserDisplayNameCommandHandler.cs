using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Application.Users.Repositories;
using MediatR;

namespace IdentityIssuer.Application.Users.Commands.UpdateUserDisplayName
{
    public class UpdateUserDisplayNameCommandHandler : AsyncRequestHandler<UpdateUserDisplayNameCommand>
    {
        private readonly IUserRepository userRepository;

        public UpdateUserDisplayNameCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        protected override async Task Handle(UpdateUserDisplayNameCommand request, CancellationToken cancellationToken)
        {
            await userRepository.UpdateUserDisplayName(request.UserGuid, request.Name);
        }
    }
}