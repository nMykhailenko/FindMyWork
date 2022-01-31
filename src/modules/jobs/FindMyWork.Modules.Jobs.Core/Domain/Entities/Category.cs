namespace FindMyWork.Modules.Jobs.Core.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    
    public DateTimeOffset CreatedAt { get; set; }
    public  bool? Deleted { get; set; }

    public virtual ICollection<JobInformation>? JobInformations { get; set; }
}