using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Auth.Commands
{
    public class RegisterUserWithCredentialsCommand : CommandBase
    {
        public RegisterUserWithCredentialsCommand(
            string userGuid, 
            string email, 
            string displayName, 
            string password, 
            TenantContextData tenant)
        {
            UserGuid = userGuid;
            Email = email;
            DisplayName = displayName;
            Password = password;
            Tenant = tenant;
        }

        public string UserGuid { get;  }
        public string Email { get;  }
        public string DisplayName { get;  }
        public string Password { get;  }
        public TenantContextData Tenant { get;  }
    }
}