using FindMyWork.Shared.Infrastructure.Database.Repositories;
using File = FindMyWork.Modules.Files.Core.Domain.Entities.File;

namespace FindMyWork.Modules.Files.Core.Application.Common.Contracts.Database;

public interface IFileRepository : IRepository
{
    Task<File> AddAsync(File file, CancellationToken cancellationToken);
    Task<File?> GetByIdAndUserIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
}