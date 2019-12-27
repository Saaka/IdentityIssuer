using System;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants;
using IdentityIssuer.Application.Users;
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
        readonly IUsersProvider usersProvider;
        readonly ITenantProvider tenantProvider;

        public ContextDataProvider(ITenantProvider tenantProvider, IUsersProvider usersProvider)
        {
            this.tenantProvider = tenantProvider;
            this.usersProvider = usersProvider;
        }

        public async Task<UserContextData> GetUser(HttpContext context)
        {
            var userGuid = GetUserCodeFromContext(context);
            var userId = await usersProvider.GetUserId(userGuid);
            
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
            var tenant = await tenantProvider.GetTenantAsync(tenantCode);
            return tenant.Id;
        }

        private string GetUserCodeFromContext(HttpContext context)
        {
            if (context.User?.Claims == null || !context.User.HasClaim(x => x.Type == ClaimTypes.NameIdentifier))
                throw new UserClaimMissingException();

            var userCode = context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return userCode;
        }

        private string GetTenantCodeFromContext(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey(IdentityIssuerHeaders.TenantHeader))
                throw new TenantHeaderMissingException();

            return context.Request.Headers[IdentityIssuerHeaders.TenantHeader];
        }
    }
}