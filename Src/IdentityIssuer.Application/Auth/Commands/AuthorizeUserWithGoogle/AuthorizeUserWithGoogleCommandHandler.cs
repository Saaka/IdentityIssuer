using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Auth.Repositories;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Requests;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Application.Users.Models;
using IdentityIssuer.Application.Users.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;

namespace IdentityIssuer.Application.Auth.Commands.AuthorizeUserWithGoogle
{
    public class AuthorizeUserWithGoogleCommandHandler
        : RequestHandler<AuthorizeUserWithGoogleCommand, AuthorizationData>
    {
        private readonly IGoogleApiClient _googleApiClient;
        private readonly ITenantProviderSettingsRepository _providerSettingsRepository;
        private readonly ITenantsRepository _tenantsRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IAvatarRepository _avatarRepository;
        private readonly IJwtTokenFactory _jwtTokenFactory;
        private readonly IGuid _guid;
        private readonly IMapper _mapper;

        public AuthorizeUserWithGoogleCommandHandler(
            IGoogleApiClient googleApiClient,
            ITenantProviderSettingsRepository providerSettingsRepository,
            ITenantsRepository tenantsRepository,
            IAuthRepository authRepository,
            IAvatarRepository avatarRepository,
            IJwtTokenFactory jwtTokenFactory,
            IGuid guid,
            IMapper mapper)
        {
            _googleApiClient = googleApiClient;
            _providerSettingsRepository = providerSettingsRepository;
            _tenantsRepository = tenantsRepository;
            _authRepository = authRepository;
            _avatarRepository = avatarRepository;
            _jwtTokenFactory = jwtTokenFactory;
            _guid = guid;
            _mapper = mapper;
        }

        public override async Task<RequestResult<AuthorizationData>> Handle(AuthorizeUserWithGoogleCommand request,
            CancellationToken cancellationToken)
        {
            var tokenInfo = await _googleApiClient.GetTokenInfoAsync(request.Token);
            var (hasError, result) = await ValidateTokenWithProviderSettings(tokenInfo, request.Tenant);
            if (hasError)
                return result;
            
            if (await _authRepository.GoogleUserExists(tokenInfo.ExternalUserId, request.Tenant.TenantId))
                return await UpdateExistingUser(tokenInfo, request.Tenant);

            if (await _authRepository.IsEmailRegisteredForTenant(tokenInfo.Email, request.Tenant.TenantId))
                return await AddGoogleToExistingUser(tokenInfo, request.Tenant);

            return await CreateNewGoogleUser(tokenInfo, request.Tenant);
        }

        private async Task<RequestResult<AuthorizationData>> CreateNewGoogleUser(TokenInfo tokenInfo, TenantContextData requestTenant)
        {
            var userGuid = _guid.GetGuid();

            var userData = _mapper.Map<CreateUserDto>(tokenInfo);
            userData.UserGuid = userGuid;
            userData.TenantId = requestTenant.TenantId;
            userData.AvatarType = AvatarType.Google;

            var user = await _authRepository.CreateGoogleUser(userData);
            await _avatarRepository
                .StoreAvatar(user.Id, AvatarType.Google, tokenInfo.ImageUrl);
            
            var data = await GetAuthorizationData(requestTenant, user);
            return RequestResult<AuthorizationData>
                .Success(data);
        }

        private async Task<RequestResult<AuthorizationData>> AddGoogleToExistingUser(TokenInfo tokenInfo, TenantContextData requestTenant)
        {
            var user = await _authRepository
                .AddGoogleLoginToUser(requestTenant.TenantId, tokenInfo.Email, tokenInfo.ExternalUserId);
            await _avatarRepository
                .StoreAvatar(user.Id, AvatarType.Google, tokenInfo.ImageUrl);

            var data = await GetAuthorizationData(requestTenant, user);
            return RequestResult<AuthorizationData>
                .Success(data);
        }

        private async Task<RequestResult<AuthorizationData>> UpdateExistingUser(TokenInfo tokenInfo, TenantContextData requestTenant)
        {
            var user = await _authRepository
                .GetUserByEmail(tokenInfo.Email, requestTenant.TenantId);
            await _avatarRepository
                .StoreAvatar(user.Id, AvatarType.Google, tokenInfo.ImageUrl);

            var data = await GetAuthorizationData(requestTenant, user);
            return RequestResult<AuthorizationData>
                .Success(data);
        }

        private async Task<AuthorizationData> GetAuthorizationData(TenantContextData requestTenant, TenantUser user)
        {
            var tenantSettings = await _tenantsRepository.GetTenantSettings(requestTenant.TenantId);
            var token = _jwtTokenFactory.Create(user, tenantSettings, requestTenant.TenantCode);

            return new AuthorizationData
            {
                Token = token,
                User = _mapper.Map<UserDto>(user)
            };
        }

        private async Task<(bool hasError, RequestResult<AuthorizationData> result)> ValidateTokenWithProviderSettings(TokenInfo tokenInfo, TenantContextData tenant)
        {
            var providerSettings = await _providerSettingsRepository
                .GetProviderSettings(tenant.TenantId, AuthProviderType.Google);

            if (providerSettings == null)
            {
                var result = RequestResult<AuthorizationData>
                    .Failure(ErrorCode.TenantProviderSettingsNotFound,
                        new {tenantCode = tenant.TenantCode, providerType = AuthProviderType.Google});
                return (true, result);
            }
            if (tokenInfo == null || tokenInfo.ClientId != providerSettings.Identifier)
            {
                var result = RequestResult<AuthorizationData>
                    .Failure(ErrorCode.InvalidProviderToken,
                        new {tenantCode = tenant.TenantCode, providerType = AuthProviderType.Google});
                return (true, result);
            }

            return (false, null);
        }
    }
}