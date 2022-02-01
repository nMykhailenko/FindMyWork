using FindMyWork.Shared.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace FindMyWork.Shared.Application.Models.RequestModels;

public record UploadFileRequest
{
    public UploadFileRequest(IFormFile file, DocumentType type)
    {
        File = file;
        Type = type;
    }

    public IFormFile File { get; init; }
    public DocumentType Type { get; set; }
};