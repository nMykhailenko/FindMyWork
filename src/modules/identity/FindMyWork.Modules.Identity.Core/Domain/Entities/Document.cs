using FindMyWork.Shared.Application.Enums;

namespace FindMyWork.Modules.Identity.Core.Domain.Entities;

public class Document
{
    public Guid Id { get; set; }
    public string Url { get; set; } = null!;
    public Guid FileRefId { get; set; }
    public DocumentType Type { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool? Deleted { get; set; }
    
    public AcceptedDocument AcceptedDocument { get; set; } = null!;
}