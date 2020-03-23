using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using MediatR;

namespace IdentityIssuer.Persistence.Behaviors
{
    public class TransactionScopeBehavior<TRequest, TResponse> : MediatR.IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await next();

                transactionScope.Complete();
                return result;
            }
        }
    }
}