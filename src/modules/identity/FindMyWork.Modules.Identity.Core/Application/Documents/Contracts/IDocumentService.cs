using FindMyWork.Modules.Users.Core.Application.Documents.Models.RequestModels;

namespace FindMyWork.Modules.Users.Core.Application.Documents.Contracts;

public interface IDocumentService
{
    Task<bool> AssignDocumentAsync(
        AssignDocumentRequest request, 
        Guid userId, 
        CancellationToken cancellationToken);
}