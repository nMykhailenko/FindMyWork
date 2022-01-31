using FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.RequestModels;
using OneOf;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.ResponseModels;
using FindMyWork.Shared.Application.Models.ErrorModels;
using FindMyWork.Shared.Application.Models.ResponseModels;

namespace FindMyWork.Modules.Jobs.Core.Application.Jobs.Contracts;

public interface IJobService
{
    Task<OneOf<JobResponse, EntityNotFound>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<JobResponse> PostJobAsync(
        Guid employerId, 
        AddJobRequest request,
        CancellationToken cancellationToken);

    Task<PaginatedResponse<IEnumerable<JobResponse>?>> GetByFilter(
        int page,
        int take,
        string route,
        CancellationToken cancellationToken);
}