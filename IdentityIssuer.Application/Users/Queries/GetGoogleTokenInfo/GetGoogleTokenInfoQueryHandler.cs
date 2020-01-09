using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Application.Users.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;
using MediatR;

namespace IdentityIssuer.Application.Users.Queries.GetGoogleTokenInfo
{
    public class
        GetGoogleTokenInfoQueryHandler : IRequestHandler<GetGoogleTokenInfoQuery, GetGoogleTokenInfoQueryResult>
    {
        private readonly IGoogleApiClient googleApiClient;
        private readonly ITenantProviderSettingsRepository providerSettingsRepository;
        private readonly IUserRepository userRepository;

        public GetGoogleTokenInfoQueryHandler(
            IGoogleApiClient googleApiClient,
            ITenantProviderSettingsRepository providerSettingsRepository,
            IUserRepository userRepository)
        {
            this.googleApiClient = googleApiClient;
            this.providerSettingsRepository = providerSettingsRepository;
            this.userRepository = userRepository;
        }

        public async Task<GetGoogleTokenInfoQueryResult> Handle(GetGoogleTokenInfoQuery request,
            CancellationToken cancellationToken)
        {
            var tokenInfo = await googleApiClient.GetTokenInfoAsync(request.Token);
            var providerSettings = await providerSettingsRepository
                .GetProviderSettings(request.Tenant.TenantId, AuthProviderType.Google);

            if (providerSettings == null)
                throw new TenantSettingsNotFoundException(request.Tenant.TenantCode);
            if (tokenInfo == null || tokenInfo.ClientId != providerSettings.Identifier)
                throw new InvalidProviderTokenException(AuthProviderType.Google, request.Tenant.TenantCode);

            var isEmailRegistered = false;
            var googleUserExists = await userRepository
                .GoogleUserExists(tokenInfo.ExternalUserId, request.Tenant.TenantId);
            if (googleUserExists)
                isEmailRegistered = true;
            else
                isEmailRegistered = await userRepository
                    .IsEmailRegisteredForTenant(tokenInfo.Email, request.Tenant.TenantId);

            return new GetGoogleTokenInfoQueryResult
            {
                TokenInfo = tokenInfo,
                IsEmailRegistered = isEmailRegistered,
                IsGoogleUserRegistered = googleUserExists
            };
        }
    }
}