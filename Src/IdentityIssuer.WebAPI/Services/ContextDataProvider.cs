using System;
using System.Linq;
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
        Task<AdminContextData> GetAdmin(HttpContext context);
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
            var userGuid = GetUserGuidFromContext(context);
            var user = await _usersProvider.GetUser(userGuid);

            var tenant = await GetTenant(context);

            return new UserContextData(user.Id, userGuid, user.IsAdmin, tenant);
        }

        public async Task<TenantContextData> GetTenant(HttpContext context)
        {
            var tenantCode = GetTenantCodeFromContext(context);
            var tenantId = await GetTenantId(tenantCode);

            return new TenantContextData(tenantId, tenantCode);
        }

        public async Task<AdminContextData> GetAdmin(HttpContext context)
        {
            var userGuid = GetUserGuidFromContext(context);
            var user = await _usersProvider.GetUser(userGuid);

            if (user == null)
                throw new DomainException(ErrorCode.UserNotFound);

            return user.IsAdmin
                ? new AdminContextData(AdminContextType.User, user.Id)
                : new AdminContextData(AdminContextType.None);
        }

        private async Task<int> GetTenantId(string tenantCode)
        {
            var tenant = await _tenantProvider.GetTenantAsync(tenantCode);
            return tenant.Id;
        }

        private Guid GetUserGuidFromContext(HttpContext context)
        {
            if (context.User?.Claims == null || !(context.User?.Claims).Any())
                throw new DomainException(ErrorCode.TenantHeaderMissing);
            else if (!context.User.HasClaim(x => x.Type == ClaimTypes.NameIdentifier))
                throw new DomainException(ErrorCode.UserClaimMissing);

            var guid = context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return new Guid(guid);
        }

        private string GetTenantCodeFromContext(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey(IdentityIssuerHeaders.TenantHeader))
                throw new DomainException(ErrorCode.TenantHeaderMissing);

            return context.Request.Headers[IdentityIssuerHeaders.TenantHeader];
        }
    }
}