using System.ComponentModel.DataAnnotations.Schema;

namespace FindMyWork.Modules.Jobs.Core.Domain.Entities;

public class JobInformation
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int NumberEmployeesToHire { get; set; }
    public decimal SalaryPerEmployee { get; set; }
    public double TotalHoursToWork { get; set; }
    public DateTimeOffset StartsOn { get; set; }
    public DateTimeOffset EndsOn { get; set; }
    
    
    public bool? Deleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
    
    [ForeignKey("JobId")]
    public Guid JobId { get; set; }
    public Job Job { get; set; } = null!;

    public Guid LocationId { get; set; }
    public Address Location { get; set; } = null!;
    
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    // TODO Contact Person should be Employer.
    public Guid ContactPersonId { get; set; }
}