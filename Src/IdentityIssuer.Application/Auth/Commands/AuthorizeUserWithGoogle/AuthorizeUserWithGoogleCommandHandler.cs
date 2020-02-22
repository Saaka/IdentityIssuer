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
        private readonly IGoogleApiClient googleApiClient;
        private readonly ITenantProviderSettingsRepository providerSettingsRepository;
        private readonly ITenantsRepository tenantsRepository;
        private readonly IAuthRepository authRepository;
        private readonly IJwtTokenFactory jwtTokenFactory;
        private readonly IGuid guid;
        private readonly IMapper mapper;

        public AuthorizeUserWithGoogleCommandHandler(
            IGoogleApiClient googleApiClient,
            ITenantProviderSettingsRepository providerSettingsRepository,
            ITenantsRepository tenantsRepository,
            IAuthRepository authRepository,
            IJwtTokenFactory jwtTokenFactory,
            IGuid guid,
            IMapper mapper)
        {
            this.googleApiClient = googleApiClient;
            this.providerSettingsRepository = providerSettingsRepository;
            this.tenantsRepository = tenantsRepository;
            this.authRepository = authRepository;
            this.jwtTokenFactory = jwtTokenFactory;
            this.guid = guid;
            this.mapper = mapper;
        }

        public async Task<AuthUserResult> Handle(AuthorizeUserWithGoogleCommand request,
            CancellationToken cancellationToken)
        {
            var tokenInfo = await googleApiClient.GetTokenInfoAsync(request.Token);
            await ValidateTokenWithProviderSettings(tokenInfo, request.Tenant);

            if (await authRepository.GoogleUserExists(tokenInfo.ExternalUserId, request.Tenant.TenantId))
                return await UpdateExistingUser(tokenInfo, request.Tenant);

            if (await authRepository.IsEmailRegisteredForTenant(tokenInfo.Email, request.Tenant.TenantId))
                return await AddGoogleToExistingUser(tokenInfo, request.Tenant);

            return await CreateNewGoogleUser(tokenInfo, request.Tenant);
        }

        private async Task<AuthUserResult> CreateNewGoogleUser(TokenInfo tokenInfo, TenantContextData requestTenant)
        {
            var userGuid = guid.GetNormalizedGuid();

            var userProperties = mapper.Map<CreateUserDto>(tokenInfo);
            userProperties.UserGuid = userGuid;
            userProperties.TenantId = requestTenant.TenantId;

            var user = await authRepository.CreateGoogleUser(userProperties);
            return await AuthUserResult(requestTenant, user);
        }

        private async Task<AuthUserResult> AddGoogleToExistingUser(TokenInfo tokenInfo, TenantContextData requestTenant)
        {
            var user = await authRepository
                .AddGoogleLoginToUser(requestTenant.TenantId, tokenInfo.Email, tokenInfo.ExternalUserId, tokenInfo.ImageUrl);

            return await AuthUserResult(requestTenant, user);
        }

        private async Task<AuthUserResult> UpdateExistingUser(TokenInfo tokenInfo, TenantContextData requestTenant)
        {
            var user = await authRepository
                .UpdateExistingGoogleUser(requestTenant.TenantId, tokenInfo.Email, tokenInfo.ImageUrl);

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

        private async Task ValidateTokenWithProviderSettings(TokenInfo tokenInfo, TenantContextData tenant)
        {
            var providerSettings = await providerSettingsRepository
                .GetProviderSettings(tenant.TenantId, AuthProviderType.Google);

            if (providerSettings == null)
                throw new TenantProviderSettingsNotFoundException(tenant.TenantCode, AuthProviderType.Google);
            if (tokenInfo == null || tokenInfo.ClientId != providerSettings.Identifier)
                throw new InvalidProviderTokenException(AuthProviderType.Google, tenant.TenantCode);
        }
    }
}