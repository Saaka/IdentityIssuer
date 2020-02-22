using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Users.Repositories;
using MediatR;

namespace IdentityIssuer.Application.Auth.Commands.RegisterUserWithCredentials
{
    public class RegisterUserWithCredentialsCommandHandler : AsyncRequestHandler<RegisterUserWithCredentialsCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly IProfileImageUrlProvider profileImageUrlProvider;

        public RegisterUserWithCredentialsCommandHandler(
            IUserRepository userRepository,
            IProfileImageUrlProvider profileImageUrlProvider)
        {
            this.userRepository = userRepository;
            this.profileImageUrlProvider = profileImageUrlProvider;
        }

        protected override async Task Handle(RegisterUserWithCredentialsCommand request,
            CancellationToken cancellationToken)
        {
            var imageUrl = profileImageUrlProvider.GetImageUrl(request.Email);
            var user = await userRepository.CreateUser(new CreateUserDto
            {
                Email = request.Email,
                Password = request.Password,
                DisplayName = request.DisplayName,
                ImageUrl = imageUrl,
                TenantId = request.Tenant.TenantId,
                UserGuid = request.UserGuid
            });
        }
    }
}