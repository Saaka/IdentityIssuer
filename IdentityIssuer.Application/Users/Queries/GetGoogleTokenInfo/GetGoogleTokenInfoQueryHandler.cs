using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;
using MediatR;

namespace IdentityIssuer.Application.Users.Queries.GetGoogleTokenInfo
{
    public class GetGoogleTokenInfoQueryHandler : IRequestHandler<GetGoogleTokenInfoQuery, TokenInfo>
    {
        private readonly IGoogleApiClient googleApiClient;
        private readonly ITenantProviderSettingsRepository providerSettingsRepository;

        public GetGoogleTokenInfoQueryHandler(
            IGoogleApiClient googleApiClient,
            ITenantProviderSettingsRepository providerSettingsRepository)
        {
            this.googleApiClient = googleApiClient;
            this.providerSettingsRepository = providerSettingsRepository;
        }

        public async Task<TokenInfo> Handle(GetGoogleTokenInfoQuery request, CancellationToken cancellationToken)
        {
            var tokenInfo = await googleApiClient.GetTokenInfoAsync(request.Token);
            var providerSettings = await providerSettingsRepository
                .GetProviderSettings(request.Tenant.TenantId, AuthProviderType.Google);

            if (tokenInfo.ClientId != providerSettings.Identifier)
                throw new InvalidProviderTokenException(AuthProviderType.Google);

            return tokenInfo;
        }
    }
}