using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Auth.Repositories;
using IdentityIssuer.Application.Services;
using MediatR;

namespace IdentityIssuer.Application.Auth.Commands.RegisterUserWithCredentials
{
    public class RegisterUserWithCredentialsCommandHandler : AsyncRequestHandler<RegisterUserWithCredentialsCommand>
    {
        private readonly IAuthRepository authRepository;
        private readonly IProfileImageUrlProvider profileImageUrlProvider;

        public RegisterUserWithCredentialsCommandHandler(
            IAuthRepository authRepository,
            IProfileImageUrlProvider profileImageUrlProvider)
        {
            this.authRepository = authRepository;
            this.profileImageUrlProvider = profileImageUrlProvider;
        }

        protected override async Task Handle(RegisterUserWithCredentialsCommand request,
            CancellationToken cancellationToken)
        {
            var imageUrl = profileImageUrlProvider.GetImageUrl(request.Email);
            await authRepository.CreateUser(new CreateUserDto
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