using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Users.Commands
{
    public class UpdateUserDisplayNameCommand : CommandBase
    {
        public UpdateUserDisplayNameCommand(
            string name,
            UserContextData tenant)
        {
            Name = name;
            User = tenant;
        }

        public string Name { get; }
        public UserContextData User { get; }
    }
}