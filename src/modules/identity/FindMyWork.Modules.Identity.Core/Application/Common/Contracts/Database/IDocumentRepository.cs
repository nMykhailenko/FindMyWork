using FindMyWork.Modules.Identity.Core.Domain.Entities;
using FindMyWork.Shared.Infrastructure.Database.Repositories;

namespace FindMyWork.Modules.Identity.Core.Application.Common.Contracts.Database;

public interface IDocumentRepository : IRepository
{
    Task<Document> AddAsync(Document document, CancellationToken cancellationToken);
    Task<AcceptedDocument?> GetAcceptedDocument(Guid documentId, CancellationToken cancellationToken);

}