using FindMyWork.Modules.Jobs.Core.Domain.Entities;
using FindMyWork.Shared.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FindMyWork.Modules.Jobs.Core.Infrastructure.Persistence;

internal class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public virtual DbSet<Job> Jobs { get; set; } = null!;
    public virtual DbSet<JobInformation> JobInformations { get; set; } = null!;
    public virtual DbSet<Address> Addresses { get; set; } = null!;
    public virtual DbSet<JobStatusInfo> JobStatusInfos { get; set; } = null!;
}