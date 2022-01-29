using FindMyWork.Modules.Jobs.Core.Domain.Enums;

namespace FindMyWork.Modules.Jobs.Core.Domain.Entities;

public class Job
{
    public Guid Id { get; set; }
    public JobStatus Status { get; set; } = JobStatus.Draft;
    
    public bool? Deleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
    
    public Guid? JobInformationId { get; set; }
    public JobInformation? JobInformation { get; set; }
    
    public Guid EmployerId { get; set; }
}