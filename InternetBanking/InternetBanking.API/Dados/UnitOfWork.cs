using InternetBanking.API.Entidades;
using InternetBanking.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;

namespace InternetBanking.API.Dados;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IExecutionStrategy GetExecutionStrategy()
    {
        return _context.Database.CreateExecutionStrategy();
    }
    public TransactionScope BeginTransaction()
    {
        var options = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted
        };
        return new TransactionScope(TransactionScopeOption.Required, options, TransactionScopeAsyncFlowOption.Enabled);
    }

    public async Task Commit(TransactionScope scope)
    {
        scope.Complete();
    }

    public void Rollback()
    {

    }
}
