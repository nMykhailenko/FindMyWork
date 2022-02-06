using System.ComponentModel.DataAnnotations.Schema;

namespace FindMyWork.Modules.Users.Core.Domain.Entities;

public class UserRole
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public long RoleId { get; set; }
    public Role Role { get; set; } = null!;
}