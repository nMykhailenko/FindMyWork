using FindMyWork.Modules.Jobs.Core.Domain.Enums;

namespace FindMyWork.Modules.Jobs.Core.Domain.Entities;

public class JobStatusInfo
{
    public Guid Id { get; set; }
    public JobStatus OldStatus { get; set; }
    public JobStatus CurrentStatus { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; }
    public Guid InitiatorId { get; set; }
    
    public Guid JobId { get; set; }
    public Job Job { get; set; } = null!;
}