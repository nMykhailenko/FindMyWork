using FindMyWork.Shared.Application.Enums;

namespace FindMyWork.Modules.Users.Core.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Mobile { get; set; } = null!;
    
    public bool Verified { get; set; }
    public UserType Type { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool? Deleted { get; set; }

    public virtual ICollection<AcceptedDocument>? Documents { get; set; }
}