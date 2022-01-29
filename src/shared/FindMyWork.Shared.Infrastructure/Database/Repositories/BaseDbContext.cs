using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FindMyWork.Shared.Infrastructure.Database.Repositories;

 public class BaseDbContext : DbContext, IUnitOfWork
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="options"></param>
        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }
        
        #region IUnitOfWork implementation

        async Task<IUnitOfWorkTransaction> IUnitOfWork.BeginTransactionAsync(CancellationToken cancellationToken)
        {
            IDbContextTransaction dbContextTransaction = await Database.BeginTransactionAsync(cancellationToken);

            return new UnitOfWorkTransaction(dbContextTransaction);
        }

        public async Task WrapWithTransactionAsync(Func<Task> actionAsync, CancellationToken cancellationToken = default)
        {
            await using var transaction = await Database.BeginTransactionAsync(cancellationToken);            
            try
            {
                await actionAsync();
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        #endregion
    }