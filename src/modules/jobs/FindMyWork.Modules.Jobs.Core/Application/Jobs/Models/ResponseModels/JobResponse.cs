namespace FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.ResponseModels;

public record JobResponse
{
    public Guid Id { get; init; }

    public string Status { get; init; } = null!;
    
    public Guid? JobInformationId { get; init; }
    public JobInformationResponse? JobInformation { get; init; }
    
    public Guid EmployerId { get; init; }
} 