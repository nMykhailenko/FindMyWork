using FindMyWork.Shared.Application.Enums;
using FindMyWork.Shared.Application.Models.RequestModels;
using FindMyWork.Shared.Application.Models.ResponseModels;
using OneOf;

namespace FindMyWork.Shared.Application.Contracts;

public interface IBlobStorageService
{
    Task<OneOf<SuccessBlobResponse, ErrorResponse>> UploadAsync(
        UploadBlobRequest request,
        CancellationToken cancellationToken);

    Task<OneOf<byte[], ErrorResponse>> DownloadFileAsync(
        string fileName,
        DocumentType documentType,
        CancellationToken cancellationToken);

    Task<OneOf<SuccessBlobResponse, ErrorResponse>> GetFileAsync(
        string fileName,
        DocumentType documentType,
        CancellationToken cancellationToken);
}