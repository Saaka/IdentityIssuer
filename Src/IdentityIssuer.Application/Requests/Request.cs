using System;
using MediatR;

namespace IdentityIssuer.Application.Requests
{
    public interface IRequestBase
    {
        Guid RequestGuid { get; }
    }
    
    public abstract class Request<TResult> : IRequest<RequestResult<TResult>>, IRequestBase
    {
        public Guid RequestGuid { get; private set; }

        protected Request()
        {
            RequestGuid = Guid.NewGuid();
        }

        public Request<TResult> WithRequestGuid(Guid guid)
        {
            RequestGuid = guid;
            return this;
        }
    }

    public abstract class Request : Request<Guid>
    {
    }
}