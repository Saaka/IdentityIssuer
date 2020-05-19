using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using IdentityIssuer.Common.Requests;
using MediatR;

namespace IdentityIssuer.Persistence.Behaviors
{
    public class TransactionScopeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : RequestResult
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await next();

                if (result.IsSuccess)
                    transactionScope.Complete();
                
                return result;
            }
        }
    }
}