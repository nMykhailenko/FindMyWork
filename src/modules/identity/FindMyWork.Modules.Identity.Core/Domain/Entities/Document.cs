using FindMyWork.Shared.Application.Enums;

namespace FindMyWork.Modules.Users.Core.Domain.Entities;

public class Document
{
    public Guid Id { get; set; }
    public string Url { get; set; } = null!;
    public DocumentType Type { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool? Deleted { get; set; }
    
    public AcceptedDocument AcceptedDocument { get; set; } = null!;
}