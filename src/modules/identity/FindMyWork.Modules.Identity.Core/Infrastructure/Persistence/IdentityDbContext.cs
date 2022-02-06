using FindMyWork.Modules.Users.Core.Domain.Entities;
using FindMyWork.Shared.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FindMyWork.Modules.Users.Core.Infrastructure.Persistence;

public class IdentityDbContext : BaseDbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}