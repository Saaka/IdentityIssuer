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

namespace IdentityIssuer.Application.Auth.Commands.AuthorizeUserWithFacebook
{
    public class AuthorizeUserWithFacebookCommandHandler
        : IRequestHandler<AuthorizeUserWithFacebookCommand, AuthUserResult>
    {
        private readonly IFacebookApiClient _facebookApiClient;
        private readonly ITenantProviderSettingsRepository _providerSettingsRepository;
        private readonly ITenantsRepository _tenantsRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IJwtTokenFactory _jwtTokenFactory;
        private readonly IGuid _guid;
        private readonly IMapper _mapper;

        public AuthorizeUserWithFacebookCommandHandler(
            IFacebookApiClient facebookApiClient,
            ITenantProviderSettingsRepository providerSettingsRepository,
            ITenantsRepository tenantsRepository,
            IAuthRepository authRepository,
            IJwtTokenFactory jwtTokenFactory,
            IGuid guid,
            IMapper mapper)
        {
            _facebookApiClient = facebookApiClient;
            _providerSettingsRepository = providerSettingsRepository;
            _tenantsRepository = tenantsRepository;
            _authRepository = authRepository;
            _jwtTokenFactory = jwtTokenFactory;
            _guid = guid;
            _mapper = mapper;
        }

        public async Task<AuthUserResult> Handle(AuthorizeUserWithFacebookCommand request,
            CancellationToken cancellationToken)
        {
            var providerSettings = await GetTenantProviderSettings(request.Tenant);

            var tokenInfo = await _facebookApiClient
                .GetTokenInfoAsync(request.Token, providerSettings.Identifier, providerSettings.Key);
            ValidateTokenWithProviderSettings(tokenInfo, request.Tenant, providerSettings);

            if (await _authRepository.FacebookUserExists(tokenInfo.ExternalUserId, request.Tenant.TenantId))
                return await UpdateExistingUser(tokenInfo, request.Tenant);

            if (await _authRepository.IsEmailRegisteredForTenant(tokenInfo.Email, request.Tenant.TenantId))
                return await AddFacebookToExistingUser(tokenInfo, request.Tenant);

            return await CreateNewFacebookUser(tokenInfo, request.Tenant);
        }

        private async Task<AuthUserResult> CreateNewFacebookUser(TokenInfo tokenInfo, TenantContextData requestTenant)
        {
            var userGuid = _guid.GetNormalizedGuid();

            var userProperties = _mapper.Map<CreateUserDto>(tokenInfo);
            userProperties.UserGuid = userGuid;
            userProperties.TenantId = requestTenant.TenantId;

            var user = await _authRepository.CreateFacebookUser(userProperties);
            return await AuthUserResult(requestTenant, user);
        }

        private async Task<AuthUserResult> AddFacebookToExistingUser(TokenInfo tokenInfo,
            TenantContextData requestTenant)
        {
            var user = await _authRepository
                .AddFacebookLoginToUser(requestTenant.TenantId, tokenInfo.Email, tokenInfo.ExternalUserId,
                    tokenInfo.ImageUrl);

            return await AuthUserResult(requestTenant, user);
        }

        private async Task<AuthUserResult> UpdateExistingUser(TokenInfo tokenInfo, TenantContextData requestTenant)
        {
            var user = await _authRepository
                .UpdateExistingFacebookUser(requestTenant.TenantId, tokenInfo.Email, tokenInfo.ImageUrl);

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

        private async Task<TenantProviderSettings> GetTenantProviderSettings(TenantContextData tenant)
        {
            var providerSettings = await _providerSettingsRepository
                .GetProviderSettings(tenant.TenantId, AuthProviderType.Facebook);
            if (providerSettings == null)
                throw new DomainException(ExceptionCode.TenantProviderSettingsNotFound,
                    new {tenantCode = tenant.TenantCode, providerType = AuthProviderType.Facebook});

            return providerSettings;
        }

        private void ValidateTokenWithProviderSettings(
            TokenInfo tokenInfo, TenantContextData tenant, TenantProviderSettings providerSettings)
        {
            if (tokenInfo == null || providerSettings == null || tokenInfo.ClientId != providerSettings.Identifier)
                throw new DomainException(ExceptionCode.InvalidProviderToken,
                    new {tenantCode = tenant.TenantCode, providerType = AuthProviderType.Google});
        }
    }
}