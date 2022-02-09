using FindMyWork.Modules.Identity.Core.Application.Documents.Models.RequestModels;
using FindMyWork.Modules.Identity.Core.Application.Documents.Models.ResponseModels;
using FindMyWork.Shared.Application.Models.ErrorModels;
using OneOf;

namespace FindMyWork.Modules.Identity.Core.Application.Documents.Contracts;

public interface IDocumentService
{
    Task<bool> AssignDocumentAsync(
        AssignDocumentRequest request, 
        Guid userId, 
        CancellationToken cancellationToken);

    Task<OneOf<AcceptedDocumentResponse, EntityNotFound>> AcceptDocumentAsync(
        Guid documentId,
        Guid signerId,
        CancellationToken cancellationToken);
}