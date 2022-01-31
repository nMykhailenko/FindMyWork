namespace FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.RequestModels;

public record AddJobRequest
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int NumberEmployeesToHire { get; set; }
    public decimal SalaryPerEmployee { get; set; }
    public double TotalHoursToWork { get; set; }
    public DateTimeOffset StartsOn { get; set; }
    public DateTimeOffset EndsOn { get; set; }
    
    public AddAddressRequest Location { get; set; } = null!;

    public Guid ContactPersonId { get; set; }
    public Guid CategoryId { get; set; }

}