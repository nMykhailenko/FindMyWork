namespace FindMyWork.Shared.Application.Models.ResponseModels;

public record PaginatedResponse<TResponse>
{
    public PaginatedResponse(
        TResponse? data, 
        int pageNumber, 
        int pageSize,
        int totalPages,
        int totalRecords,
        string firstPage,
        string lastPage,
        string? nextPage,
        string? previousPage)
    {
        Succeeded = true;
        Message = string.Empty;
        Errors = null;
        Data = data;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = totalPages;
        TotalRecords = totalRecords;
        FirstPage = firstPage;
        LastPage = lastPage;
        NextPage = nextPage;
        PreviousPage = previousPage;
    }
    
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public string FirstPage { get; init; } = null!;
    public string LastPage { get; init; } = null!;

    public int TotalPages { get; init; }
    public int TotalRecords { get; init; }
    public string? NextPage { get; init; }
    public string? PreviousPage { get; init; }

    public TResponse? Data { get; init; }
    
    public bool Succeeded { get; set; }
    public string[]? Errors { get; set; }
    public string? Message { get; set; }
    
    
};