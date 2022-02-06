using Microsoft.AspNetCore.Identity;

namespace FindMyWork.Modules.Users.Core.Domain.Entities;

public class Role : IdentityRole<Guid>
{
    public List<UserRole> UserRoles { get; set; } = null!;
}