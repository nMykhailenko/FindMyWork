using FindMyWork.Shared.Infrastructure.Database.Repositories;
using FindMyWork.Modules.Jobs.Core.Domain.Entities;

namespace FindMyWork.Modules.Jobs.Core.Application.Common.Contracts.Database;

public interface IJobRepository : IRepository
{
    Task<Job?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Domain.Entities.Job> AddAsync(Job job, CancellationToken cancellationToken);
}