namespace IdentityIssuer.Common.Enums
{
    public enum ErrorCode
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
        /// <summary>
        /// Tenant was not found for given criteria.
        /// </summary>
        TenantNotFound = 5,
        /// <summary>
        /// Tenant header not found in request.
        /// </summary>
        TenantHeaderMissing = 6,
        /// <summary>
        /// Tenant provider settings were not found for given tenant and provider.
        /// </summary>
        TenantProviderSettingsNotFound = 7,
        /// <summary>
        /// Tenant settings were not found for given tenant.
        /// </summary>
        TenantSettingsNotFound = 8,
        /// <summary>
        /// Claim with user code is missing and user can't be authenticated.
        /// </summary>
        UserClaimMissing = 9,
        /// <summary>
        /// Could not find user for given criteria.
        /// </summary>
        UserNotFound = 10,
        /// <summary>
        /// Given provider security token is not valid for tenant.
        /// </summary>
        InvalidProviderToken = 11,
        /// <summary>
        /// Tenant with given code already exists
        /// </summary>
        TenantAlreadyExistsForCode = 12,
        /// <summary>
        /// Update of user properties failed
        /// </summary>
        UserUpdateFailed = 13,
        /// <summary>
        /// There was an error while validating the request. Usually linked with other, more specific error code.
        /// </summary>
        ValidationError,
    }
}