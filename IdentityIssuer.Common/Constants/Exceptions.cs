namespace IdentityIssuer.Common.Constants
{
    public class Exceptions
    {
        public class UnauthorizedAccessException
        {
            public const string MissingTenantContextData = "Could not establish Tenant from current context";
            public const string KidMissmatch = "Current tenant does not match JWT Kid header";
            public const string MissingTenantTokenSecret = "Could not get current Tenant token secret key";
        }
    }
}