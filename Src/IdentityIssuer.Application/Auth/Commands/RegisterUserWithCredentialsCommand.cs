using IdentityIssuer.Application.Models;
using System;

namespace IdentityIssuer.Application.Auth.Commands
{
    public class RegisterUserWithCredentialsCommand : CommandBase
    {
        public RegisterUserWithCredentialsCommand(
            Guid userGuid, 
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

        public Guid UserGuid { get;  }
        public string Email { get;  }
        public string DisplayName { get;  }
        public string Password { get;  }
        public TenantContextData Tenant { get;  }
    }
}