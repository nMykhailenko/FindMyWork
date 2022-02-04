using FindMyWork.Shared.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace FindMyWork.Modules.Files.Core.Application.Files.Models.RequestModels;

public record UploadFileRequest(IFormFile File, DocumentType Type);