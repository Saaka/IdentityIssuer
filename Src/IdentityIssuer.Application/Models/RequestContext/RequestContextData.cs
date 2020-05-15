using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Models.RequestContext
{
    public class RequestContextData
    {
        public AdminContextType ContextType { get; }
        public bool IsTenantContext => Tenant != null;
        public bool IsUserContext => User != null;
        
        public TenantContext Tenant { get; private set; }
        public UserContext User { get; private set; }

        public RequestContextData WithTenantContext(TenantContext tenant)
        {
            Tenant = tenant;
            return this;
        }

        public RequestContextData WithUserContext(UserContext user)
        {
            User = user;
            return this;
        }
    }
}