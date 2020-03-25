using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Auth.Repositories;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Users.Models;
using IdentityIssuer.Application.Users.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Auth.Commands.RegisterUserWithCredentials
{
    public class
        RegisterUserWithCredentialsCommandHandler : RequestHandler<RegisterUserWithCredentialsCommand, UserDto>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IAvatarRepository _avatarRepository;
        private readonly IProfileImageUrlProvider _profileImageUrlProvider;
        private readonly IMapper _mapper;

        public RegisterUserWithCredentialsCommandHandler(
            IAuthRepository authRepository,
            IAvatarRepository avatarRepository,
            IProfileImageUrlProvider profileImageUrlProvider,
            IMapper mapper)
        {
            _authRepository = authRepository;
            _avatarRepository = avatarRepository;
            _profileImageUrlProvider = profileImageUrlProvider;
            _mapper = mapper;
        }

        public override async Task<RequestResult<UserDto>> Handle(RegisterUserWithCredentialsCommand request,
            CancellationToken cancellationToken)
        {
            var imageUrl = _profileImageUrlProvider.GetImageUrl(request.Email);
            var user = await _authRepository.CreateUser(new CreateUserData
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

            return RequestResult<UserDto>
                .Success(_mapper.Map<UserDto>(user));
        }
    }
}