using System;

namespace IdentityIssuer.Common.Exceptions
{
    public class UserNotFoundException : ArgumentException
    {
        public UserNotFoundException(string userCode)
            : base($"User {userCode} not found")
        {
        }
        
        public UserNotFoundException(int userId, int tenantId)
            : base($"User with id {userId} and tenant id {tenantId} not found")
        {
        }
    }
}