namespace FindMyWork.Shared.Application.Models.ResponseModels;

public record UploadedFileResponse
{
    public UploadedFileResponse(string url, string sasToken)
    {
        Url = url;
        SasToken = sasToken;
    }

    public string Url { get; }
    public string SasToken { get; }
};