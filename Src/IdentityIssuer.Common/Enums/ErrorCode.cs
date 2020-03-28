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
        TenantNotFound = 5,
        /// <summary>
        /// Tenant header not found in request.
        /// </summary>
        TenantHeaderMissing = 6,
        TenantProviderSettingsNotFound = 7,
        TenantSettingsNotFound = 8,
        /// <summary>
        /// Claim with user code is missing and user can't be authenticated.
        /// </summary>
        UserClaimMissing = 9,
        UserNotFound = 10,
        /// <summary>
        /// Given provider security token is not valid for tenant.
        /// </summary>
        InvalidProviderToken = 11,
        TenantAlreadyExistsForCode = 12,
        UserUpdateFailed = 13,
        /// <summary>
        /// There was an error while validating the request. Usually linked with other, more specific error code.
        /// </summary>
        ValidationError = 14,
        /// <summary>
        /// Creation of tenant failed.
        /// </summary>
        CreateTenantFailed = 15,
        TenantProviderSettingsAlreadyExists = 16,
        CreateTenantProviderSettingsFailed = 17,
        UpdateTenantSettingsFailed = 18,
        UpdateTenantProviderSettingsFailed = 19,
        DeleteTenantProviderSettingsFailed = 20,
    }
}