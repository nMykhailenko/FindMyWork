namespace FindMyWork.Modules.Jobs.Core.Domain.Enums;

public enum JobStatus : byte
{
    None = 0,
    Draft = 1, 
    Created = 2,
    Pending = 3,
    Hired = 4,
    Active = 5,
    Canceled = 6,
    Completed = 7,
    FailedToComplete = 8,
    Archived = 9 
}