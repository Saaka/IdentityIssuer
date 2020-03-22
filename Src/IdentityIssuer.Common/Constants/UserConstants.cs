namespace IdentityIssuer.Common.Constants
{
    public static class UserConstants
    {
        public const int MinPasswordLength = 6;
        public const int MaxPasswordLength = 128;
        public const int MinDisplayNameLength = 3;
        public const int MaxDisplayNameLength = 20;
        public const int MinEmailLength = 5;
        public const int MaxEmailLength = 256;
        public const int MaxImageUrlLength = 1024;
        public const int MaxGoogleIdLength = 64;
        public const string AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    }
}