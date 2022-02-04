using FindMyWork.Shared.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using File = FindMyWork.Modules.Files.Core.Domain.Entities.File;

namespace FindMyWork.Modules.Files.Core.Infrastructure.Persistence;

internal class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public virtual DbSet<File> Files { get; set; } = null!;
}