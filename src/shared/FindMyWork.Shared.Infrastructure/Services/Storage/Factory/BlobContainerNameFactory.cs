using FindMyWork.Shared.Application.Enums;

namespace FindMyWork.Shared.Infrastructure.Services.Storage.Factory;

internal class BlobContainerNameFactory : IBlobContainerNameFactory
{
    private readonly Dictionary<DocumentType, IBlobContainerName> _factories;

    public BlobContainerNameFactory()
    {
        _factories = new()
        {
            {DocumentType.LocalPassport, new PassportBlobContainer()},
            {DocumentType.InternationalPassport, new InternationalPassportBlobContainer()},
            {DocumentType.DriverLicence, new DriverLicenseBlobContainer()}
        };
    }


    public IBlobContainerName Create(DocumentType documentType) 
        => _factories[documentType];
}