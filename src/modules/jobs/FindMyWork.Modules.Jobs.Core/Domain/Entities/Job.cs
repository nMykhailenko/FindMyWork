using System.ComponentModel.DataAnnotations.Schema;
using FindMyWork.Modules.Jobs.Core.Domain.Enums;

namespace FindMyWork.Modules.Jobs.Core.Domain.Entities;

public class Job
{
    public Guid Id { get; set; }

    public JobStatus Status { get; set; }
    
    public bool? Deleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
    
    public JobInformation JobInformation { get; set; } = null!;

    public Guid EmployerId { get; set; }

    public virtual ICollection<JobStatusInfo> JobStatusInfos { get; set; } = null!;
}