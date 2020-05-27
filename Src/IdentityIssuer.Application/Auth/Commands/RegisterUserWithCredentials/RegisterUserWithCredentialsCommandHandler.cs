using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Auth.Events;
using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Auth.Repositories;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Users.Models;
using IdentityIssuer.Application.Users.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests;
using MediatR;

namespace IdentityIssuer.Application.Auth.Commands.RegisterUserWithCredentials
{
    public class
        RegisterUserWithCredentialsCommandHandler : Common.Requests.RequestHandler<RegisterUserWithCredentialsCommand, UserDto>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IAvatarRepository _avatarRepository;
        private readonly IProfileImageUrlProvider _profileImageUrlProvider;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public RegisterUserWithCredentialsCommandHandler(
            IAuthRepository authRepository,
            IAvatarRepository avatarRepository,
            IProfileImageUrlProvider profileImageUrlProvider,
            IMapper mapper,
            IMediator mediator)
        {
            _authRepository = authRepository;
            _avatarRepository = avatarRepository;
            _profileImageUrlProvider = profileImageUrlProvider;
            _mapper = mapper;
            _mediator = mediator;
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
                TenantId = request.RequestContext.Tenant.TenantId,
                UserGuid = request.UserGuid,
                AvatarType = AvatarType.Gravatar,
            });
            await _avatarRepository
                .StoreAvatar(user.Id, AvatarType.Gravatar, imageUrl);

            await _mediator
                .Publish(new UserRegisteredWithCredentials(user.UserGuid), cancellationToken);
            
            return RequestResult<UserDto>
                .Success(_mapper.Map<UserDto>(user));
        }
    }
}