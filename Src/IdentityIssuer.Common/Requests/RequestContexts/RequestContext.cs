using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Common.Requests.RequestContexts
{
    public class RequestContext
    {
        public AdminContextType AdminContextType { get; private set; } = AdminContextType.None;
        public bool IsTenantContext => Tenant != null;
        public bool IsUserContext => User != null;

        public TenantContext Tenant { get; private set; }
        public UserContext User { get; private set; }

        public RequestContext WithTenantContext(TenantContext tenant)
        {
            Tenant = tenant;
            return this;
        }

        public RequestContext WithUserContext(UserContext user)
        {
            User = user;
            if (User.IsAdmin)
                AdminContextType = AdminContextType.User;
            
            return this;
        }

        public RequestContext WithSystemAdminContext()
        {
            AdminContextType = AdminContextType.System;
            return this;
        }
    }
}