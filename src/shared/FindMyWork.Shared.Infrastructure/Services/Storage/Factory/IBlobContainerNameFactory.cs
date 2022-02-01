using FindMyWork.Shared.Application.Enums;

namespace FindMyWork.Shared.Infrastructure.Services.Storage.Factory;

public interface IBlobContainerNameFactory
{
    IBlobContainerName Create(DocumentType documentType);
}