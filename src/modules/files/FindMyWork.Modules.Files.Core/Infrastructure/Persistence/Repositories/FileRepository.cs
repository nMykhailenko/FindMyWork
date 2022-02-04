using FindMyWork.Modules.Files.Core.Application.Common.Contracts.Database;
using FindMyWork.Shared.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using File = FindMyWork.Modules.Files.Core.Domain.Entities.File;

namespace FindMyWork.Modules.Files.Core.Infrastructure.Persistence.Repositories;

internal class FileRepository : Repository, IFileRepository
{
    private ApplicationDbContext DbContext => (UnitOfWork as ApplicationDbContext)!;

    public FileRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<File> AddAsync(File file, CancellationToken cancellationToken)
    {
        var addedFile = await DbContext.Files.AddAsync(file, cancellationToken);

        return addedFile.Entity;
    }

    public Task<File?> GetByIdAndUserIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        return DbContext.Files
            .FirstOrDefaultAsync(x => x.Id == id && x.CreatedByUserId == userId, cancellationToken);
    }
}