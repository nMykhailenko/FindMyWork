namespace FindMyWork.Modules.Jobs.Core.Domain.Entities;

public class Address
{
    public Guid Id { get; set; }
    public string AddressLine { get; set; } = null!;
    public string City { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string Country { get; set; } = null!;
    
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool? Deleted { get; set; }
}