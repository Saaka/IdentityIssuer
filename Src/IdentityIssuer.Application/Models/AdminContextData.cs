using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Models
{
    public class AdminContextData
    {
        public AdminContextType ContextType { get; }
        public int? UserId { get; }

        public AdminContextData(
            AdminContextType contextType,
            int? userId = null)
        {
            ContextType = contextType;
            UserId = userId;
        }
    }
}