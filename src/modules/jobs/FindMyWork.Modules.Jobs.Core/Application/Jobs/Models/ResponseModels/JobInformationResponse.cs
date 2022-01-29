namespace FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.ResponseModels;

public record JobInformationResponse
{
    public Guid Id { get; init; }

    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public int QuantityToHire { get; init; }
    public decimal SalaryPerEmployee { get; init; }
    public double TotalHoursToWork { get; init; }
    public DateTimeOffset StartsOn { get; init; }
    public DateTimeOffset EndsOn { get; init; }

    public AddressResponse Location { get; init; } = null!;
    
    // TODO Contact Person should be Employer.
    public Guid ContactPersonId { get; init; }
}