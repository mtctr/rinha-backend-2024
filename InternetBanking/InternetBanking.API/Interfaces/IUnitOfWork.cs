using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;

namespace InternetBanking.API.Interfaces
{
    public interface IUnitOfWork
    {
        IExecutionStrategy GetExecutionStrategy();
        TransactionScope BeginTransaction();
        Task Commit(TransactionScope scope);
        void Rollback();

    }
}
