using System;
using MediatR;

namespace IdentityIssuer.Common.Requests
{
    public interface IRequestBase
    {
        Guid RequestGuid { get; }
        Guid CorrelationId { get; }
    }
    
    public abstract class Request<TResult> : IRequest<RequestResult<TResult>>, IRequestBase
    {
        public Guid RequestGuid { get; private set; }
        public Guid CorrelationId { get; private set; }

        protected Request()
        {
            RequestGuid = Guid.NewGuid();
            CorrelationId = Guid.NewGuid();
        }

        public Request<TResult> WithCorrelationId(Guid guid)
        {
            CorrelationId = guid;
            return this;
        }
    }

    public abstract class Request : Request<Guid>
    {
    }
}