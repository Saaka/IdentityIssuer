using System.Security.Claims;
using System.Threading.Tasks;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants;
using IdentityIssuer.Application.Users;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;
using IdentityIssuer.WebAPI.Configurations;
using Microsoft.AspNetCore.Http;

namespace IdentityIssuer.WebAPI.Services
{
    public interface IContextDataProvider
    {
        Task<UserContextData> GetUser(HttpContext context);
        Task<TenantContextData> GetTenant(HttpContext context);
    }

    public class ContextDataProvider : IContextDataProvider
    {
        private readonly IUsersProvider _usersProvider;
        private readonly ITenantProvider _tenantProvider;

        public ContextDataProvider(ITenantProvider tenantProvider, IUsersProvider usersProvider)
        {
            _tenantProvider = tenantProvider;
            _usersProvider = usersProvider;
        }

        public async Task<UserContextData> GetUser(HttpContext context)
        {
            var userGuid = GetUserCodeFromContext(context);
            var userId = await _usersProvider.GetUserId(userGuid);
            
            var tenant = await GetTenant(context);

            return new UserContextData(userId, userGuid, tenant);
        }

        public async Task<TenantContextData> GetTenant(HttpContext context)
        {
            var tenantCode = GetTenantCodeFromContext(context);
            var tenantId = await GetTenantId(tenantCode);

            return new TenantContextData(tenantId, tenantCode);
        }

        private async Task<int> GetTenantId(string tenantCode)
        {
            var tenant = await _tenantProvider.GetTenantAsync(tenantCode);
            return tenant.Id;
        }

        private string GetUserCodeFromContext(HttpContext context)
        {
            if (context.User?.Claims == null || !context.User.HasClaim(x => x.Type == ClaimTypes.NameIdentifier))
                throw new DomainException(ExceptionCode.UserClaimMissing);

            var userCode = context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return userCode;
        }

        private string GetTenantCodeFromContext(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey(IdentityIssuerHeaders.TenantHeader))
                throw new DomainException(ExceptionCode.TenantHeaderMissing);

            return context.Request.Headers[IdentityIssuerHeaders.TenantHeader];
        }
    }
}