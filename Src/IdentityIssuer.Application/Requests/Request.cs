using System;
using MediatR;

namespace IdentityIssuer.Application.Requests
{
    public abstract class Request<TResult> : IRequest<RequestResult<TResult>>
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
}