using FindMyWork.Shared.Application.Enums;

namespace FindMyWork.Modules.Files.Core.Application.Files.Models.ResponseModels;

public record SuccessFileResponse(
    Guid Id,
    DocumentType Type,
    string FileName,
    string Url);