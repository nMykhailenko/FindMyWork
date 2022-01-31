using FindMyWork.Shared.Application.Models.ResponseModels;

namespace FindMyWork.Modules.Jobs.Core.Application.Jobs.Contracts;

public interface IPaginationHelper
{
    PaginatedResponse<IEnumerable<T>?> CreatePagedResponse<T>(
        IEnumerable<T>? pagedData,
        int page,
        int take,
        int totalRecords,
        string route);
}