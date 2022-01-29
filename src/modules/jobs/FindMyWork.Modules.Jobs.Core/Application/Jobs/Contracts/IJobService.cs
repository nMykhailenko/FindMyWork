using OneOf;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.ResponseModels;
using FindMyWork.Shared.Application.Models.ErrorModels;

namespace FindMyWork.Modules.Jobs.Core.Application.Jobs.Contracts;

public interface IJobService
{
    Task<OneOf<JobResponse, EntityNotFound>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}