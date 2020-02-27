using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Application.Auth.Repositories;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;
using MediatR;

namespace IdentityIssuer.Application.Auth.Queries.GetGoogleTokenInfo
{
    public class
        GetGoogleTokenInfoQueryHandler : IRequestHandler<GetGoogleTokenInfoQuery, GetGoogleTokenInfoQueryResult>
    {
        private readonly IGoogleApiClient googleApiClient;
        private readonly ITenantProviderSettingsRepository providerSettingsRepository;
        private readonly IAuthRepository authRepository;

        public GetGoogleTokenInfoQueryHandler(
            IGoogleApiClient googleApiClient,
            ITenantProviderSettingsRepository providerSettingsRepository,
            IAuthRepository authRepository)
        {
            this.googleApiClient = googleApiClient;
            this.providerSettingsRepository = providerSettingsRepository;
            this.authRepository = authRepository;
        }

        public async Task<GetGoogleTokenInfoQueryResult> Handle(GetGoogleTokenInfoQuery request,
            CancellationToken cancellationToken)
        {
            var tokenInfo = await googleApiClient.GetTokenInfoAsync(request.Token);
            var providerSettings = await providerSettingsRepository
                .GetProviderSettings(request.Tenant.TenantId, AuthProviderType.Google);

            if (providerSettings == null)
                throw new DomainException(ExceptionCode.TenantSettingsNotFound,
                    new {tenantCode = request.Tenant.TenantCode});
            if (tokenInfo == null || tokenInfo.ClientId != providerSettings.Identifier)
                throw new DomainException(ExceptionCode.InvalidProviderToken,
                    new {providerType = AuthProviderType.Google, tenantCode = request.Tenant.TenantCode});

            var isEmailRegistered = false;
            var googleUserExists = await authRepository
                .GoogleUserExists(tokenInfo.ExternalUserId, request.Tenant.TenantId);
            if (googleUserExists)
                isEmailRegistered = true;
            else
                isEmailRegistered = await authRepository
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