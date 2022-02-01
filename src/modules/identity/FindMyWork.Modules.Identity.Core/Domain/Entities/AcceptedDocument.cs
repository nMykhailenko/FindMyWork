namespace FindMyWork.Modules.Users.Core.Domain.Entities;

public class AcceptedDocument
{
    public Guid Id { get; set; }
    public bool Accepted { get; set; }
    public Guid? SignedById { get; set; }
    public Guid DocumentId { get; set; }
    public DateTimeOffset SignedAt { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }

    public User? SignedBy { get; set; }
    public Document Document { get; set; } = null!;
}