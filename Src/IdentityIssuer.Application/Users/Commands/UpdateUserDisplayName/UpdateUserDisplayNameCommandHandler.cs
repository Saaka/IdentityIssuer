using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace IdentityIssuer.Application.Users.Commands.UpdateUserDisplayName
{
    public class UpdateUserDisplayNameCommandHandler : AsyncRequestHandler<UpdateUserDisplayNameCommand>
    {
        protected override async Task Handle(UpdateUserDisplayNameCommand request, CancellationToken cancellationToken)
        {
            
        }
    }
}