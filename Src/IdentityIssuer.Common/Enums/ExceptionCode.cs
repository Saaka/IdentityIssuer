namespace IdentityIssuer.Common.Enums
{
    public enum ExceptionCode
    {
        /// <summary>
        /// Facebook token is missing email permission.
        /// </summary>
        FacebookTokenEmailPermissionRequired = 1,
        ///<summary>
        /// Could not establish Tenant from current context.
        /// </summary>
        MissingTenantContextData = 2,
        /// <summary>
        /// Current tenant does not match JWT Kid header.
        /// </summary>
        KidMissmatch = 3,
        /// <summary>
        /// Could not get current Tenant token secret key.
        /// </summary>
        MissingTenantTokenSecret = 4,
    }
}