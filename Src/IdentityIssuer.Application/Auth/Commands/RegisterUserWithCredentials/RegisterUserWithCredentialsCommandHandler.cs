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
        private readonly IAuthRepository _authRepository;
        private readonly IProfileImageUrlProvider _profileImageUrlProvider;

        public RegisterUserWithCredentialsCommandHandler(
            IAuthRepository authRepository,
            IProfileImageUrlProvider profileImageUrlProvider)
        {
            _authRepository = authRepository;
            _profileImageUrlProvider = profileImageUrlProvider;
        }

        protected override async Task Handle(RegisterUserWithCredentialsCommand request,
            CancellationToken cancellationToken)
        {
            var imageUrl = _profileImageUrlProvider.GetImageUrl(request.Email);
            await _authRepository.CreateUser(new CreateUserDto
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