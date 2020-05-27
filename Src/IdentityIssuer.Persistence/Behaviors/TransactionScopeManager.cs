namespace IdentityIssuer.Persistence.Behaviors
{
    public interface ITransactionScopeManager
    {
        bool IsTransactionCreatedForScope { get; }
        void SetTransactionCreated(bool isCreated);
    }
    
    public class TransactionScopeManager : ITransactionScopeManager
    {
        public bool IsTransactionCreatedForScope { get; private set; }

        public void SetTransactionCreated(bool isCreated) => IsTransactionCreatedForScope = isCreated;
    }
}