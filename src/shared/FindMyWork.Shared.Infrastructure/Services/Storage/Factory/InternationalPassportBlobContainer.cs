namespace FindMyWork.Shared.Infrastructure.Services.Storage.Factory;

internal class InternationalPassportBlobContainer : IBlobContainerName
{
    public string Get() => "international-passport";
}