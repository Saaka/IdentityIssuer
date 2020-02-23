namespace IdentityIssuer.Common.Constants
{
    public class Exceptions
    {
        public class UnauthorizedAccessException
        {
            /// <summary>
            /// Could not establish Tenant from current context
            /// </summary>
            public const string MissingTenantContextData = nameof(MissingTenantContextData);
            /// <summary>
            /// Current tenant does not match JWT Kid header
            /// </summary>
            public const string KidMissmatch = nameof(KidMissmatch);
            /// <summary>
            /// Could not get current Tenant token secret key
            /// </summary>
            public const string MissingTenantTokenSecret = nameof(MissingTenantTokenSecret);
        }
    }
}