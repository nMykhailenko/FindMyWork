using FindMyWork.Shared.Application.Enums;

namespace FindMyWork.Modules.Files.Core.Domain.Entities;

public class File
{
    public Guid Id { get; set; }
    public DocumentType Type { get; set; }
    public string Name { get; set; } = null!;
    
    public Guid CreatedByUserId { get; set; }
    public UserType CreatedByUserType { get; set; }

    public bool? Deleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
}