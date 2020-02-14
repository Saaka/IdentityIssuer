using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Users.Commands
{
    public class UpdateUserDisplayNameCommand : CommandBase
    {
        public UpdateUserDisplayNameCommand(
            string name,
            string userGuid,
            UserContextData tenant)
        {
            Name = name;
            UserGuid = userGuid;
            User = tenant;
        }

        public string Name { get; }
        public string UserGuid { get; }
        public UserContextData User { get; }
    }
}