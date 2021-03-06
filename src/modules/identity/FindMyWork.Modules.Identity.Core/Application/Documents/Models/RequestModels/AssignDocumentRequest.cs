using FindMyWork.Shared.Application.Enums;

namespace FindMyWork.Modules.Identity.Core.Application.Documents.Models.RequestModels;

public record AssignDocumentRequest
{
    public string Url { get; init; } = null!;
    public DocumentType Type { get; init; } 
};