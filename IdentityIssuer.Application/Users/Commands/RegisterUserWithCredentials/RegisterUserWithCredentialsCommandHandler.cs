using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Application.Services;
using MediatR;

namespace IdentityIssuer.Application.Users.Commands.RegisterUserWithCredentials
{
    public class RegisterUserWithCredentialsCommandHandler : AsyncRequestHandler<RegisterUserWithCredentialsCommand>
    {
        private readonly IGuid guid;

        public RegisterUserWithCredentialsCommandHandler(IGuid guid)
        {
            this.guid = guid;
        }

        protected override async Task Handle(RegisterUserWithCredentialsCommand request, CancellationToken cancellationToken)
        {
            
        }
    }
}