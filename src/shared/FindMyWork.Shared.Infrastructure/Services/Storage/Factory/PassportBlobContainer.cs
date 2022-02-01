namespace FindMyWork.Shared.Infrastructure.Services.Storage.Factory;

internal class PassportBlobContainer : IBlobContainerName
{
    public string Get() => "passport";
}