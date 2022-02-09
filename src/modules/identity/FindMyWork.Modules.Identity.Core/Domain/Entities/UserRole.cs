namespace FindMyWork.Modules.Identity.Core.Domain.Entities;

public class UserRole
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public long RoleId { get; set; }
    public Role Role { get; set; } = null!;
}