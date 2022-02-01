namespace FindMyWork.Shared.Infrastructure.Services.Storage.Factory;

internal class DriverLicenseBlobContainer : IBlobContainerName
{
    public string Get() => "driver-license";
}