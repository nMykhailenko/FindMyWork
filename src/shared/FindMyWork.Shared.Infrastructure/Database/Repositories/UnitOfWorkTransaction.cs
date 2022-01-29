using Microsoft.EntityFrameworkCore.Storage;

namespace FindMyWork.Shared.Infrastructure.Database.Repositories;

class UnitOfWorkTransaction: IUnitOfWorkTransaction
{
    private readonly IDbContextTransaction _dbContextTransaction;
    private bool _disposedValue;

    public UnitOfWorkTransaction(IDbContextTransaction dbContextTransaction)
    {
        _dbContextTransaction = dbContextTransaction;
    }

    #region IDisposable and IDisposableAsync implementation 

    public ValueTask DisposeAsync()
    {
        return _dbContextTransaction.DisposeAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue) return;
        
        if (disposing)
        {
            _dbContextTransaction.Dispose();
        }

        _disposedValue = true;
    }
    
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    #endregion

    Task IUnitOfWorkTransaction.CommitAsync(CancellationToken cancellationToken)
    {
        return _dbContextTransaction.CommitAsync(cancellationToken);
    }

    Task IUnitOfWorkTransaction.RollbackAsync(CancellationToken cancellationToken)
    {
        return _dbContextTransaction.CommitAsync(cancellationToken);
    }
}