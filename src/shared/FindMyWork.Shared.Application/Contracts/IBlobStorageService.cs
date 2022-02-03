using FindMyWork.Shared.Application.Enums;
using FindMyWork.Shared.Application.Models.RequestModels;
using FindMyWork.Shared.Application.Models.ResponseModels;
using OneOf;

namespace FindMyWork.Shared.Application.Contracts;

public interface IBlobStorageService
{
    Task<OneOf<SuccessFileResponse, ErrorResponse>> UploadAsync(
        UploadFileRequest request,
        CancellationToken cancellationToken);

    Task<OneOf<byte[], ErrorResponse>> DownloadFileAsync(
        string fileName,
        DocumentType documentType,
        CancellationToken cancellationToken);

    Task<OneOf<SuccessFileResponse, ErrorResponse>> GetFileAsync(
        string fileName,
        DocumentType documentType,
        CancellationToken cancellationToken);
}