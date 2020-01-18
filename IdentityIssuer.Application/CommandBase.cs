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

    public class CommandBase<T> : IRequest<T>, ICommandBase
    {
        protected CommandBase()
        {
            CommandUuid = new Guid();
        }

        public Guid CommandUuid { get; }
    }
}