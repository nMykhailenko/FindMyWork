using FindMyWork.Modules.Jobs.Core.Application.Common.Contracts;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Contracts;
using FindMyWork.Shared.Application.Models.ResponseModels;

namespace FindMyWork.Modules.Jobs.Core.Application.Jobs.Helpers;

public class PaginationHelper : IPaginationHelper
{
    private readonly IUriService _uriService;

    public PaginationHelper(IUriService uriService)
    {
        _uriService = uriService;
    }

    public PaginatedResponse<IEnumerable<T>?> CreatePagedResponse<T>(
        IEnumerable<T>? pagedData,
        int page, 
        int take, 
        int totalRecords, 
        string route)
    {
        var totalPages = ((double)totalRecords / take);
        var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
        var nextPage =
            page >= 1 && page < roundedTotalPages
                ? _uriService.GetPageUri(page + 1, take, route)
                : null;
        var previousPage =
            page - 1 >= 1 && page <= roundedTotalPages
                ? _uriService.GetPageUri(page- 1, take, route)
                : null;
        var firstPage = _uriService.GetPageUri(page, take, route);
        var lastPage = _uriService.GetPageUri(roundedTotalPages, take, route);
        var response = new PaginatedResponse<IEnumerable<T>?>(
            pagedData, 
            page, 
            take,
            roundedTotalPages,
            totalRecords,
            firstPage.ToString(),
            lastPage.ToString(),
            nextPage?.ToString(),
            previousPage?.ToString());

        return response;
    }
}