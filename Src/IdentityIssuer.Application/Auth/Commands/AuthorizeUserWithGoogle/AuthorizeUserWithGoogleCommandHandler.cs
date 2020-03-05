using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Auth.Repositories;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Application.Users.Models;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;
using MediatR;

namespace IdentityIssuer.Application.Auth.Commands.AuthorizeUserWithGoogle
{
    public class AuthorizeUserWithGoogleCommandHandler
        : IRequestHandler<AuthorizeUserWithGoogleCommand, AuthUserResult>
    {
        private readonly IGoogleApiClient _googleApiClient;
        private readonly ITenantProviderSettingsRepository _providerSettingsRepository;
        private readonly ITenantsRepository _tenantsRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IJwtTokenFactory _jwtTokenFactory;
        private readonly IGuid _guid;
        private readonly IMapper _mapper;

        public AuthorizeUserWithGoogleCommandHandler(
            IGoogleApiClient googleApiClient,
            ITenantProviderSettingsRepository providerSettingsRepository,
            ITenantsRepository tenantsRepository,
            IAuthRepository authRepository,
            IJwtTokenFactory jwtTokenFactory,
            IGuid guid,
            IMapper mapper)
        {
            _googleApiClient = googleApiClient;
            _providerSettingsRepository = providerSettingsRepository;
            _tenantsRepository = tenantsRepository;
            _authRepository = authRepository;
            _jwtTokenFactory = jwtTokenFactory;
            _guid = guid;
            _mapper = mapper;
        }

        public async Task<AuthUserResult> Handle(AuthorizeUserWithGoogleCommand request,
            CancellationToken cancellationToken)
        {
            var tokenInfo = await _googleApiClient.GetTokenInfoAsync(request.Token);
            await ValidateTokenWithProviderSettings(tokenInfo, request.Tenant);

            if (await _authRepository.GoogleUserExists(tokenInfo.ExternalUserId, request.Tenant.TenantId))
                return await UpdateExistingUser(tokenInfo, request.Tenant);

            if (await _authRepository.IsEmailRegisteredForTenant(tokenInfo.Email, request.Tenant.TenantId))
                return await AddGoogleToExistingUser(tokenInfo, request.Tenant);

            return await CreateNewGoogleUser(tokenInfo, request.Tenant);
        }

        private async Task<AuthUserResult> CreateNewGoogleUser(TokenInfo tokenInfo, TenantContextData requestTenant)
        {
            var userGuid = _guid.GetNormalizedGuid();

            var userData = _mapper.Map<CreateUserDto>(tokenInfo);
            userData.UserGuid = userGuid;
            userData.TenantId = requestTenant.TenantId;
            userData.AvatarType = AvatarType.Google;

            var user = await _authRepository.CreateGoogleUser(userData);
            return await AuthUserResult(requestTenant, user);
        }

        private async Task<AuthUserResult> AddGoogleToExistingUser(TokenInfo tokenInfo, TenantContextData requestTenant)
        {
            var user = await _authRepository
                .AddGoogleLoginToUser(requestTenant.TenantId, tokenInfo.Email, tokenInfo.ExternalUserId,
                    tokenInfo.ImageUrl);

            return await AuthUserResult(requestTenant, user);
        }

        private async Task<AuthUserResult> UpdateExistingUser(TokenInfo tokenInfo, TenantContextData requestTenant)
        {
            var user = await _authRepository
                .UpdateExistingGoogleUser(requestTenant.TenantId, tokenInfo.Email, tokenInfo.ImageUrl);

            return await AuthUserResult(requestTenant, user);
        }

        private async Task<AuthUserResult> AuthUserResult(TenantContextData requestTenant, TenantUser user)
        {
            var tenantSettings = await _tenantsRepository.GetTenantSettings(requestTenant.TenantId);
            var token = _jwtTokenFactory.Create(user, tenantSettings, requestTenant.TenantCode);

            return new AuthUserResult
            {
                Token = token,
                User = _mapper.Map<UserDto>(user)
            };
        }

        private async Task ValidateTokenWithProviderSettings(TokenInfo tokenInfo, TenantContextData tenant)
        {
            var providerSettings = await _providerSettingsRepository
                .GetProviderSettings(tenant.TenantId, AuthProviderType.Google);

            if (providerSettings == null)
                throw new DomainException(ExceptionCode.TenantProviderSettingsNotFound,
                    new {tenantCode = tenant.TenantCode, providerType = AuthProviderType.Google});
            if (tokenInfo == null || tokenInfo.ClientId != providerSettings.Identifier)
                throw new DomainException(ExceptionCode.InvalidProviderToken,
                    new {tenantCode = tenant.TenantCode, providerType = AuthProviderType.Google});
        }
    }
}