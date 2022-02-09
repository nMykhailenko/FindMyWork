using FindMyWork.Modules.Identity.Core.Domain.Entities;
using FindMyWork.Shared.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FindMyWork.Modules.Identity.Core.Infrastructure.Persistence;

public class IdentityDbContext : BaseDbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}