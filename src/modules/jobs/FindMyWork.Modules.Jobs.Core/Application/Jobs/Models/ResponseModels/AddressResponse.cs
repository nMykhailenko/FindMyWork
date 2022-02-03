namespace FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.ResponseModels;

public record AddressResponse
{
    public string AddressLine { get; init; } = null!;
    public string City { get; init; } = null!;
    public string ZipCode { get; init; } = null!;
    public string Country { get; init; } = null!;
};