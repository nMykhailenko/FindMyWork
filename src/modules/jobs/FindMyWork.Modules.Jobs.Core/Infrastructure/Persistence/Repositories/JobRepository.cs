using FindMyWork.Modules.Jobs.Core.Application.Common.Contracts.Database;
using FindMyWork.Modules.Jobs.Core.Domain.Entities;
using FindMyWork.Shared.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FindMyWork.Modules.Jobs.Core.Infrastructure.Persistence.Repositories;

internal class JobRepository : Repository, IJobRepository
{
    private JobsDbContext DbContext => (UnitOfWork as JobsDbContext)!;
    
    public JobRepository(JobsDbContext dbContext) : base(dbContext)
    {
    }

    public Task<Job?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return DbContext.Jobs
            .Include(x => x.JobStatusInfos)
            .Include(x => x.JobInformation)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Job> AddAsync(Job job, CancellationToken cancellationToken)
    {
        var addedJob = await DbContext.Jobs.AddAsync(job, cancellationToken);
        
        return addedJob.Entity;
    }

    public async Task<IEnumerable<Job>> GetPaginatedByCategoryAsync(
        Guid categoryId,
        int page, 
        int take, 
        CancellationToken cancellationToken)
    {
        return await DbContext.Jobs
            .Include(x => x.JobStatusInfos)
            .Include(x => x.JobInformation)
            .Where(x => x.Deleted == null && x.JobInformation.CategoryId == categoryId)
            .OrderBy(x => x.Id)
            .Skip((page - 1) * take)
            .Take(take)
            .ToListAsync(cancellationToken);
    }

    public Task<int> CountByCategoryAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        return DbContext.Jobs
            .Where(x => x.JobInformation.CategoryId == categoryId)
            .CountAsync(cancellationToken);
    }

    public void Update(Job job)
    {
        DbContext.Jobs.Update(job);
    }
}