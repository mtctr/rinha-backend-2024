using InternetBanking.API.Interfaces;
using System.Transactions;

namespace InternetBanking.API.Dados;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public TransactionScope BeginTransaction()
    {        
        return new TransactionScope();
    }

    public void Commit(TransactionScope scope)
    {
        _context.SaveChanges();
        scope.Complete();
    }    
}
