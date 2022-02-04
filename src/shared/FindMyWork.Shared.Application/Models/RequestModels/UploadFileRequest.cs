using FindMyWork.Shared.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace FindMyWork.Shared.Application.Models.RequestModels;

public record UploadBlobRequest(IFormFile File, DocumentType Type);