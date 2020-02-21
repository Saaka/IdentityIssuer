using System;

namespace IdentityIssuer.Common.Exceptions
{
    public class UserNotFoundException : ArgumentException
    {
        public UserNotFoundException(string userCode)
            : base($"User {userCode} not found")
        {
        }
        
        public UserNotFoundException(string email, string tenantCode)
            : base($"User with email {email} and tenant id {tenantCode} not found")
        {
        }
    }
}