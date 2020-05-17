using IdentityIssuer.Application.Models;
using System;
using IdentityIssuer.Application.Users.Models;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Auth.Commands
{
    public class RegisterUserWithCredentialsCommand : Request<UserDto>
    {
        public RegisterUserWithCredentialsCommand(
            Guid userGuid, 
            string email, 
            string displayName, 
            string password)
        {
            UserGuid = userGuid;
            Email = email;
            DisplayName = displayName;
            Password = password;
        }

        public Guid UserGuid { get;  }
        public string Email { get;  }
        public string DisplayName { get;  }
        public string Password { get;  }
        public TenantContextData Tenant { get;  }
    }
}