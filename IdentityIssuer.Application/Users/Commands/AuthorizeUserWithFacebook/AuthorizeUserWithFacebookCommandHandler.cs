using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Application.Users.Models;
using IdentityIssuer.Application.Users.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;
using MediatR;

namespace IdentityIssuer.Application.Users.Commands.AuthorizeUserWithFacebook
{
    public class
        AuthorizeUserWithFacebookCommandHandler : IRequestHandler<AuthorizeUserWithFacebookCommand, AuthUserResult>
    {
        private readonly IFacebookApiClient facebookApiClient;
        private readonly ITenantProviderSettingsRepository providerSettingsRepository;
        private readonly ITenantsRepository tenantsRepository;
        private readonly IUserRepository userRepository;
        private readonly IJwtTokenFactory jwtTokenFactory;
        private readonly IGuid guid;
        private readonly IMapper mapper;

        public AuthorizeUserWithFacebookCommandHandler(
            IFacebookApiClient facebookApiClient,
            ITenantProviderSettingsRepository providerSettingsRepository,
            ITenantsRepository tenantsRepository,
            IUserRepository userRepository,
            IJwtTokenFactory jwtTokenFactory,
            IGuid guid,
            IMapper mapper)
        {
            this.facebookApiClient = facebookApiClient;
            this.providerSettingsRepository = providerSettingsRepository;
            this.tenantsRepository = tenantsRepository;
            this.userRepository = userRepository;
            this.jwtTokenFactory = jwtTokenFactory;
            this.guid = guid;
            this.mapper = mapper;
        }

        public async Task<AuthUserResult> Handle(AuthorizeUserWithFacebookCommand request,
            CancellationToken cancellationToken)
        {
            var providerSettings = await GetTenantProviderSettings(request.Tenant);

            var tokenInfo = await facebookApiClient
                .GetTokenInfoAsync(request.Token, providerSettings.Identifier, providerSettings.Identifier);
            ValidateTokenWithProviderSettings(tokenInfo, request.Tenant, providerSettings);

            if (await userRepository.FacebookUserExists(tokenInfo.ExternalUserId, request.Tenant.TenantId))
                return await UpdateExistingUser(tokenInfo, request.Tenant);

            if (await userRepository.IsEmailRegisteredForTenant(tokenInfo.Email, request.Tenant.TenantId))
                return await AddFacebookToExistingUser(tokenInfo, request.Tenant);

            return await CreateNewFacebookUser(tokenInfo, request.Tenant);
        }

        private async Task<AuthUserResult> CreateNewFacebookUser(TokenInfo tokenInfo, TenantContextData requestTenant)
        {
            var userGuid = guid.GetNormalizedGuid();

            var userProperties = mapper.Map<CreateUserDto>(tokenInfo);
            userProperties.UserGuid = userGuid;
            userProperties.TenantId = requestTenant.TenantId;

            var user = await userRepository.CreateFacebookUser(userProperties);
            return await AuthUserResult(requestTenant, user);
        }

        private async Task<AuthUserResult> AddFacebookToExistingUser(TokenInfo tokenInfo, TenantContextData requestTenant)
        {
            var user = await userRepository
                .AddFacebookLoginToUser(requestTenant.TenantId, tokenInfo.Email, tokenInfo.ExternalUserId,
                    tokenInfo.ImageUrl);

            return await AuthUserResult(requestTenant, user);
        }

        private async Task<AuthUserResult> UpdateExistingUser(TokenInfo tokenInfo, TenantContextData requestTenant)
        {
            var user = await userRepository
                .UpdateExistingFacebookUser(requestTenant.TenantId, tokenInfo.Email, tokenInfo.ImageUrl);

            return await AuthUserResult(requestTenant, user);
        }

        private async Task<AuthUserResult> AuthUserResult(TenantContextData requestTenant, TenantUser user)
        {
            var tenantSettings = await tenantsRepository.GetTenantSettings(requestTenant.TenantId);
            var token = jwtTokenFactory.Create(user, tenantSettings, requestTenant.TenantCode);

            return new AuthUserResult
            {
                Token = token,
                User = mapper.Map<UserDto>(user)
            };
        }

        private async Task<TenantProviderSettings> GetTenantProviderSettings(TenantContextData tenant)
        {
            var providerSettings = await providerSettingsRepository
                .GetProviderSettings(tenant.TenantId, AuthProviderType.Facebook);
            if (providerSettings == null)
                throw new TenantProviderSettingsNotFoundException(tenant.TenantCode, AuthProviderType.Facebook);
            
            return providerSettings;
        }

        private void ValidateTokenWithProviderSettings(
            TokenInfo tokenInfo, TenantContextData tenant, TenantProviderSettings providerSettings)
        {
            if (tokenInfo == null || tokenInfo.ClientId != providerSettings.Identifier)
                throw new InvalidProviderTokenException(AuthProviderType.Facebook, tenant.TenantCode);
        }
    }
}