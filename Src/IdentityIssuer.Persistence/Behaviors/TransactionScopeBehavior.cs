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
        private readonly ITransactionScopeManager _transactionScopeManager;

        public TransactionScopeBehavior(ITransactionScopeManager transactionScopeManager)
        {
            _transactionScopeManager = transactionScopeManager;
        }    
        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (_transactionScopeManager.IsTransactionCreatedForScope)
                return await next();

            return await WrapRequestInTransaction(next);
        }

        private async Task<TResponse> WrapRequestInTransaction(RequestHandlerDelegate<TResponse> next)
        {
            var options = new TransactionOptions
            {
                Timeout = TransactionManager.DefaultTimeout,
                IsolationLevel = IsolationLevel.Serializable
            };
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required, options, TransactionScopeAsyncFlowOption.Enabled))
            {
                _transactionScopeManager.SetTransactionCreated(true);
                var result = await next();
                if (result.IsSuccess)
                    transactionScope.Complete();

                return result;
            }
        }
    }
}