using OneOf;
using FindMyWork.Modules.Users.Core.Application.Documents.Models.RequestModels;
using FindMyWork.Modules.Users.Core.Application.Documents.Models.ResponseModels;
using FindMyWork.Shared.Application.Models.ErrorModels;

namespace FindMyWork.Modules.Users.Core.Application.Documents.Contracts;

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