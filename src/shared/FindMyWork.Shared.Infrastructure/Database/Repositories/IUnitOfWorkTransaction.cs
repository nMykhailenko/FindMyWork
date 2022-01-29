namespace FindMyWork.Shared.Infrastructure.Database.Repositories;

public interface IUnitOfWorkTransaction : IDisposable, IAsyncDisposable
{
    Task CommitAsync(CancellationToken cancellationToken);
    Task RollbackAsync(CancellationToken cancellationToken);
}