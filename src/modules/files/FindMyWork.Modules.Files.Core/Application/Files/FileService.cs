using AutoMapper;
using FindMyWork.Modules.Files.Core.Application.Common.Contracts.Database;
using FindMyWork.Modules.Files.Core.Application.Files.Contracts;
using FindMyWork.Modules.Files.Core.Application.Files.Models.RequestModels;
using FindMyWork.Modules.Files.Core.Application.Files.Models.ResponseModels;
using FindMyWork.Shared.Application.Contracts;
using FindMyWork.Shared.Application.Enums;
using FindMyWork.Shared.Application.Models.RequestModels;
using FindMyWork.Shared.Application.Models.ResponseModels;
using OneOf;
using File = FindMyWork.Modules.Files.Core.Domain.Entities.File;

namespace FindMyWork.Modules.Files.Core.Application.Files;

public class FileService : IFileService
{
    private readonly IMapper _mapper;
    private readonly IBlobStorageService _blobStorageService;
    private readonly IFileRepository _fileRepository;

    public FileService(
        IMapper mapper,
        IBlobStorageService blobStorageService, 
        IFileRepository fileRepository)
    {
        _mapper = mapper;
        _blobStorageService = blobStorageService;
        _fileRepository = fileRepository;
    }

    public async Task<OneOf<UploadFileResponse, ErrorResponse>> UploadFileAsync(
        UploadFileRequest request,
        Guid? userId,
        UserType? userType,
        CancellationToken cancellationToken)
    {

        var uploadBlobRequest = new UploadBlobRequest(request.File, request.Type);
        var blobResponse = await _blobStorageService
            .UploadAsync(uploadBlobRequest, cancellationToken);
        
        return await blobResponse.Match<Task<OneOf<UploadFileResponse, ErrorResponse>>>(
            async success =>
            {
                var file = _mapper.Map<File>((request, userId, userType));
                
                var addedFile = _fileRepository.AddAsync(file, cancellationToken);
                await _fileRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                
                var response = _mapper.Map<UploadFileResponse>((addedFile, success.Url));
                return response;
            },
            error => Task.FromResult<OneOf<UploadFileResponse, ErrorResponse>>(error));
    }
}