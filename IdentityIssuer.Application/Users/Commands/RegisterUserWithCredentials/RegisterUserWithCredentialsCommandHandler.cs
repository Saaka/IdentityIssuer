using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Users.Models;
using MediatR;

namespace IdentityIssuer.Application.Users.Commands.RegisterUserWithCredentials
{
    public class RegisterUserWithCredentialsCommandHandler : IRequestHandler<RegisterUserWithCredentialsCommand, AuthUserCommandResult>
    {
        private readonly IGuid guid;

        public RegisterUserWithCredentialsCommandHandler(IGuid guid)
        {
            this.guid = guid;
        }

        public async Task<AuthUserCommandResult> Handle(RegisterUserWithCredentialsCommand request, CancellationToken cancellationToken)
        {
            return new AuthUserCommandResult
            {
                User = new UserDto
                {
                    Email = request.Email
                }
            };
        }
    }
}