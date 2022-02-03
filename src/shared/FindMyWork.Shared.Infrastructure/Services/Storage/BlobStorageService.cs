using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using FindMyWork.Shared.Application.Contracts;
using FindMyWork.Shared.Application.Enums;
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

    public async Task<OneOf<SuccessFileResponse, ErrorResponse>> UploadAsync(
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
            return new ErrorResponse("UploadingFileIssue",
                $"Error during uploading file with name: {request.File.FileName} " +
                $"and document type: {request.Type}");
        }

        var blobSasBuilder = new BlobSasBuilder(BlobSasPermissions.Add, DateTimeOffset.UtcNow.AddMinutes(15));
        var sasToken = blobClient.GenerateSasUri(blobSasBuilder);
        var response = new SuccessFileResponse(
            blobClient.Uri.AbsolutePath,
            "token");
        return response;
    }

    public async Task<OneOf<byte[], ErrorResponse>> DownloadFileAsync(
        string fileName,
        DocumentType documentType,
        CancellationToken cancellationToken)
    {
        var containerName = _blobContainerNameFactory
            .Create(documentType)
            .Get();

        var blobContainer = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = blobContainer.GetBlobClient(fileName);

        var downloadedContent = await blobClient.DownloadAsync(cancellationToken);
        if (downloadedContent is null)
        {
            return new ErrorResponse(
                "DownloadingBlobIssue",
                $"Cannot download blob with name: {fileName} " +
                $"from container {containerName}");
        }
        
        await using var memoryStream = new MemoryStream();
        await downloadedContent.Value.Content.CopyToAsync(memoryStream, cancellationToken);
        
        return memoryStream.ToArray();
    }

    public async Task<OneOf<SuccessFileResponse, ErrorResponse>> GetFileAsync(
        string fileName,
        DocumentType documentType,
        CancellationToken cancellationToken)
    {
        var containerName = _blobContainerNameFactory
            .Create(documentType)
            .Get();

        var blobContainer = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = blobContainer.GetBlobClient(fileName);

        if (await blobClient.ExistsAsync(cancellationToken))
        {
            return new SuccessFileResponse(blobClient.Uri.AbsolutePath, "token");
        }

        return new ErrorResponse(
            "RetrieveBlobIssue",
            $"Cannot get blob with name: {fileName} " +
            $"from container {containerName}");
    }
}