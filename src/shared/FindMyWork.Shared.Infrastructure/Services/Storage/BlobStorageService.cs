using Azure.Storage.Blobs;
using FindMyWork.Shared.Application.Contracts;
using FindMyWork.Shared.Application.Models.RequestModels;
using FindMyWork.Shared.Application.Models.ResponseModels;
using FindMyWork.Shared.Infrastructure.Services.Storage.Factory;
using OneOf;

namespace FindMyWork.Shared.Infrastructure.Services.Storage;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly IBlobContainerNameFactory _blobContainerNameFactory;

    public BlobStorageService(
        BlobServiceClient blobServiceClient, 
        IBlobContainerNameFactory blobContainerNameFactory)
    {
        _blobServiceClient = blobServiceClient;
        _blobContainerNameFactory = blobContainerNameFactory;
    }

    public async Task<OneOf<UploadedFileResponse, ErrorResponse>> UploadAsync(
        UploadFileRequest request,
        CancellationToken cancellationToken)
    {
        var containerName = _blobContainerNameFactory
            .Create(request.Type)
            .Get();
        
        var blobContainer = _blobServiceClient.GetBlobContainerClient(containerName);

        var fileName = $"{Guid.NewGuid()}";
        var blobClient = blobContainer.GetBlobClient(fileName);
 
        var uploadedResult = await blobClient.UploadAsync(
            request.File.OpenReadStream(), 
            cancellationToken);

        if (uploadedResult is null)
        {
            return new ErrorResponse("UploadFile", $"Error during uploading file with name: {request.File.FileName} " +
                                               $"and document type: {request.Type}");
        }

        var response = new UploadedFileResponse("url", "token");
        return response;
    }
}