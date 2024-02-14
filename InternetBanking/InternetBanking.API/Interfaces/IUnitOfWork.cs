using System.Transactions;

namespace InternetBanking.API.Interfaces
{
    public interface IUnitOfWork
    {
        TransactionScope BeginTransaction();        
        void Commit(TransactionScope scope);
    }
}
