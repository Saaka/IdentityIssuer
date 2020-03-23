namespace IdentityIssuer.Common.Enums
{
    public enum ValidationErrorCode
    {
        UserActionNotAllowed = 1,
        TenantActionNotAllowed = 2,
        UserContextRequired = 3,
        TenantContextRequired = 4,
        AdminContextRequired = 5,
        ProviderTokenRequired = 6,
        UserNameRequired = 7,
        UserNameInvalid = 8,
        UserGuidRequired = 9,
        UserPasswordInvalid = 10,
        UserPasswordRequired = 11,
        UserEmailRequired = 12,
        UserEmailInvalid = 13,
        UserEmailNotUnique = 14,
        TenantCodeRequired = 15,
        TenantCodeInvalid = 16,
        TenantNameRequired = 17,
        TenantNameInvalid = 18,
        TenantAllowedOriginRequired = 19,
        TokenExpirationInMinutesRequired = 20,
        TokenSecretRequired = 21,
        TokenSecretInvalid = 22,
    }
}