using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace IdentityIssuer.Application.Requests
{
    public abstract class RequestHandler<TRequest> : IRequestHandler<TRequest, RequestResult>
        where TRequest : Request
    {
        public abstract Task<RequestResult> Handle(TRequest request, CancellationToken cancellationToken);
    }
    
    public abstract class RequestHandler<TRequest, TResult> : IRequestHandler<TRequest, RequestResult<TResult>>
        where TRequest : Request<TResult>
    {
        public abstract Task<RequestResult<TResult>> Handle(TRequest request, CancellationToken cancellationToken);
    }
}