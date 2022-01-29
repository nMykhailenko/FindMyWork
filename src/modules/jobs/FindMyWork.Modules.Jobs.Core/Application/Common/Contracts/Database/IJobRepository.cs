using FindMyWork.Modules.Jobs.Core.Domain.Entities;
using FindMyWork.Shared.Infrastructure.Database.Repositories;

namespace FindMyWork.Modules.Jobs.Core.Application.Common.Contracts.Database;

public interface IJobRepository : IRepository
{
    Task<Job?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Job> AddAsync(Job job, CancellationToken cancellationToken);
}