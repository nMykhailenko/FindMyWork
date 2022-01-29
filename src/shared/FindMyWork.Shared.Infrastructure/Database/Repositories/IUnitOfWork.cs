namespace FindMyWork.Shared.Infrastructure.Database.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task<IUnitOfWorkTransaction> BeginTransactionAsync(CancellationToken cancellationToken);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task WrapWithTransactionAsync(Func<Task> actionAsync, CancellationToken cancellationToken);
}
