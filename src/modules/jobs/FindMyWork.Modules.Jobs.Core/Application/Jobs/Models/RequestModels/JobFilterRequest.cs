namespace FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.RequestModels;

public record JobFilterRequest
{
    public int Page { get; init; }
    public int Take { get; init; }
    public Guid CategoryId { get; init; }
};