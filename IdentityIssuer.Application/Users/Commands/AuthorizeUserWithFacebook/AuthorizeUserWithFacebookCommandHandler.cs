using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Application.Users.Models;
using MediatR;

namespace IdentityIssuer.Application.Users.Commands.AuthorizeUserWithFacebook
{
    public class AuthorizeUserWithFacebookCommandHandler: IRequestHandler<AuthorizeUserWithFacebookCommand, AuthUserResult>
    {
        public async Task<AuthUserResult> Handle(AuthorizeUserWithFacebookCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}