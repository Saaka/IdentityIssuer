using System;

namespace IdentityIssuer.Common.Exceptions
{
    public class UserClaimMissingException : ArgumentException
    {
        public UserClaimMissingException()
            : base("Could not authenticate user")
        {
        }
    }
}