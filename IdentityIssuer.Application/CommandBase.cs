using System;
using MediatR;

namespace IdentityIssuer.Application
{
    public interface ICommandBase
    {
        Guid CommandUuid { get; }
    }

    public class CommandBase : IRequest, ICommandBase
    {
        protected CommandBase()
        {
            CommandUuid = Guid.NewGuid();
        }

        public Guid CommandUuid { get; } 
    }
}