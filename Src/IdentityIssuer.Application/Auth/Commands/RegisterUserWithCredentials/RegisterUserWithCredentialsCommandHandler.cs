using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Auth.Repositories;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Users.Repositories;
using IdentityIssuer.Common.Enums;
using MediatR;

namespace IdentityIssuer.Application.Auth.Commands.RegisterUserWithCredentials
{
    public class RegisterUserWithCredentialsCommandHandler : IRequestHandler<RegisterUserWithCredentialsCommand, RegisterUserWithCredentialsResult>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IAvatarRepository _avatarRepository;
        private readonly IProfileImageUrlProvider _profileImageUrlProvider;

        public RegisterUserWithCredentialsCommandHandler(
            IAuthRepository authRepository,
            IAvatarRepository avatarRepository,
            IProfileImageUrlProvider profileImageUrlProvider)
        {
            _authRepository = authRepository;
            _avatarRepository = avatarRepository;
            _profileImageUrlProvider = profileImageUrlProvider;
        }

        public async Task<RegisterUserWithCredentialsResult> Handle(RegisterUserWithCredentialsCommand request,
            CancellationToken cancellationToken)
        {
            var imageUrl = _profileImageUrlProvider.GetImageUrl(request.Email);
            var user = await _authRepository.CreateUser(new CreateUserDto
            {
                Email = request.Email,
                Password = request.Password,
                DisplayName = request.DisplayName,
                ImageUrl = imageUrl,
                TenantId = request.Tenant.TenantId,
                UserGuid = request.UserGuid,
                AvatarType = AvatarType.Gravatar,
            });
            await _avatarRepository
                .StoreAvatar(user.Id, AvatarType.Gravatar, imageUrl);
            
            return new RegisterUserWithCredentialsResult(true, user);
        }
    }
}