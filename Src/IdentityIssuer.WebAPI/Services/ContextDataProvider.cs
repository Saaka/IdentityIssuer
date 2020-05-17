using System;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants;
using IdentityIssuer.Application.Users;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;
using IdentityIssuer.Common.Requests.RequestContext;
using IdentityIssuer.WebAPI.Configurations;
using Microsoft.AspNetCore.Http;

namespace IdentityIssuer.WebAPI.Services
{
    public interface IContextDataProvider
    {
        Task<RequestContextData> GetRequestContext(HttpContext context);
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
            var tenant = await _tenantProvider.GetTenantAsync(tenantCode);

            return new TenantContextData(tenant.Id, tenant.Code);
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

        public async Task<AdminTenantContextData> GetAdminTenant(HttpContext context)
        {
            var tenantCode = GetTenantCodeFromContext(context);
            var tenant = await _tenantProvider.GetTenantAsync(tenantCode);

            return new AdminTenantContextData(tenant.Id, tenant.Code, tenant.IsAdminTenant);
        }

        private Guid GetUserGuidFromContext(HttpContext context)
        {
            var guid = context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return new Guid(guid);
        }

        private string GetTenantCodeFromContext(HttpContext context) =>
            context.Request.Headers[IdentityIssuerHeaders.TenantHeader];

        public async Task<RequestContextData> GetRequestContext(HttpContext context)
        {
            var requestContext = new RequestContextData();
            if (HasTenantContext(context))
                requestContext.WithTenantContext(await GetTenantContext(context));
            if (HasUserContext(context))
                requestContext.WithUserContext(await GetUserContext(context));

            return requestContext;
        }

        private async Task<TenantContext> GetTenantContext(HttpContext context)
        {
            var tenantCode = GetTenantCodeFromContext(context);
            var tenant = await _tenantProvider.GetTenantAsync(tenantCode);
            
            if(tenant == null)
                throw new DomainException(ErrorCode.TenantNotFound);

            return new TenantContext(tenant.Id, tenant.Code, tenant.IsAdminTenant);
        }

        private async Task<UserContext> GetUserContext(HttpContext context)
        {
            var userGuid = GetUserGuidFromContext(context);
            var user = await _usersProvider.GetUser(userGuid);

            if (user == null)
                throw new DomainException(ErrorCode.UserNotFound);
            
            return new UserContext(user.Id, user.UserGuid, user.IsAdmin);
        }

        private bool HasTenantContext(HttpContext context) =>
            context.Request.Headers.ContainsKey(IdentityIssuerHeaders.TenantHeader);

        private bool HasUserContext(HttpContext context)
            => context.User?.Claims != null && context.User.HasClaim(x => x.Type == ClaimTypes.NameIdentifier);
    }
}