using FindMyWork.Shared.Infrastructure.Database.Repositories;
using FindMyWork.Modules.Jobs.Core.Domain.Entities;

namespace FindMyWork.Modules.Jobs.Core.Application.Common.Contracts.Database;

internal interface IJobRepository : IRepository
{
    Task<Job?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Job> AddAsync(Job job, CancellationToken cancellationToken);

    Task<IEnumerable<Job>> GetPaginatedByCategoryAsync(Guid categoryId, int page, int take, CancellationToken cancellationToken);

    Task<int> CountByCategoryAsync(Guid categoryId, CancellationToken cancellationToken);
}