using FindMyWork.Modules.Files.Core.Application.Files.Models.RequestModels;
using FindMyWork.Modules.Files.Core.Application.Files.Models.ResponseModels;
using FindMyWork.Shared.Application.Enums;
using FindMyWork.Shared.Application.Models.ErrorModels;
using FindMyWork.Shared.Application.Models.ResponseModels;
using OneOf;

namespace FindMyWork.Modules.Files.Core.Application.Files.Contracts;

public interface IFileService
{
    Task<OneOf<UploadFileResponse, ErrorResponse>> UploadFileAsync(
        UploadFileRequest request,
        Guid userId,
        UserType userType,
        CancellationToken cancellationToken);

    Task<OneOf<SuccessBlobResponse, EntityNotFound, ErrorResponse>> GetFileAsync(
        Guid id,
        Guid userId,
        CancellationToken cancellationToken);
}