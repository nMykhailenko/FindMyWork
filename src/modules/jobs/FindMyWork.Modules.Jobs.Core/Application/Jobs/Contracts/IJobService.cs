using FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.RequestModels;
using OneOf;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.ResponseModels;
using FindMyWork.Shared.Application.Models.ErrorModels;
using FindMyWork.Shared.Application.Models.ResponseModels;
using FindMyWork.Shared.Application.Models.SuccessModels;

namespace FindMyWork.Modules.Jobs.Core.Application.Jobs.Contracts;

public interface IJobService
{
    Task<OneOf<JobResponse, EntityNotFound>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<OneOf<JobResponse, EntityNotValid>> PostJobAsync(
        Guid employerId, 
        AddJobRequest request,
        CancellationToken cancellationToken);

    Task<PaginatedResponse<IEnumerable<JobResponse>?>> GetByFilter(
        JobFilterRequest request,
        string route,
        CancellationToken cancellationToken);

    Task<OneOf<OperationSuccess, EntityNotFound>> SoftDeleteAsync(
        Guid id,
        Guid userId,
        CancellationToken cancellationToken);
}