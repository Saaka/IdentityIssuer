using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Application.Services;
using MediatR;

namespace IdentityIssuer.Application.Users.Commands.RegisterUserWithCredentials
{
    public class RegisterUserWithCredentialsCommandHandler: IRequestHandler<RegisterUserWithCredentialsCommand, AuthUserCommandResult>
    {
        private readonly IGuid guid;
        public RegisterUserWithCredentialsCommandHandler(IGuid guid)
        {
            this.guid = guid;
        }

        public async Task<AuthUserCommandResult> Handle(RegisterUserWithCredentialsCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}