using AutoMapper;
using FindMyWork.Modules.Files.Core.Application.Common.Contracts.Database;
using FindMyWork.Modules.Files.Core.Application.Files.Contracts;
using FindMyWork.Modules.Files.Core.Application.Files.Models.RequestModels;
using FindMyWork.Modules.Files.Core.Application.Files.Models.ResponseModels;
using FindMyWork.Shared.Application.Contracts;
using FindMyWork.Shared.Application.Enums;
using FindMyWork.Shared.Application.Models.ErrorModels;
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
        Guid userId,
        UserType userType,
        CancellationToken cancellationToken)
    {
        var fileName = Guid.NewGuid().ToString();
        var uploadBlobRequest = new UploadBlobRequest(request.File, request.Type, fileName);
        var blobResponse = await _blobStorageService
            .UploadAsync(uploadBlobRequest, cancellationToken);
        
        return await blobResponse.Match<Task<OneOf<UploadFileResponse, ErrorResponse>>>(
            async success =>
            {
                var file = _mapper.Map<File>((request, userId, userType, fileName));
                
                var addedFile = _fileRepository.AddAsync(file, cancellationToken);
                await _fileRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                
                var response = _mapper.Map<UploadFileResponse>((addedFile, success.Url));
                return response;
            },
            error => Task.FromResult<OneOf<UploadFileResponse, ErrorResponse>>(error));
    }

    public async Task<OneOf<SuccessBlobResponse, EntityNotFound,  ErrorResponse>> GetFileAsync(
        Guid id,
        Guid userId, 
        CancellationToken cancellationToken)
    {
        var file = await _fileRepository.GetByIdAndUserIdAsync(id, userId, cancellationToken);
        if (file is null)
        {
            return new EntityNotFound($"File with id {id} not found");
        }

        var blobResponse = await _blobStorageService.GetFileAsync(
            file.Name,
            file.Type,
            cancellationToken);

        return await blobResponse.Match(
            success => Task.FromResult<OneOf<SuccessBlobResponse, EntityNotFound, ErrorResponse>>(success),
            error => Task.FromResult<OneOf<SuccessBlobResponse, EntityNotFound, ErrorResponse>>(error));
    }
}