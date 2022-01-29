﻿using FindMyWork.Modules.Jobs.Core.Application.Common.Contracts.Database;
using FindMyWork.Modules.Jobs.Core.Domain.Entities;
using FindMyWork.Shared.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FindMyWork.Modules.Jobs.Core.Infrastructure.Persistence.Repositories;

public class JobRepository : Repository, IJobRepository
{
    private ApplicationDbContext DbContext => (UnitOfWork as ApplicationDbContext)!;
    
    public JobRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
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
        await DbContext.Jobs.AddAsync(job, cancellationToken);
        
        return job;
    }
}