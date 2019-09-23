namespace IdentityIssuer.WebAPI.Cors
{
    public class PolicyConstants
    {
        public const string PreflightPolicy = nameof(PreflightPolicy);
        public const string TenantHeader = "X-Tenant-Code";
        public const string OriginHeader = "Origin";
        public const string AccessControlRequestHeader = "Access-Control-Request-Headers";
    }
}
