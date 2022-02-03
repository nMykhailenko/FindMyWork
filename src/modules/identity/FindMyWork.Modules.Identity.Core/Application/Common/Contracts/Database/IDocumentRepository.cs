using FindMyWork.Modules.Users.Core.Domain.Entities;
using FindMyWork.Shared.Infrastructure.Database.Repositories;

namespace FindMyWork.Modules.Users.Core.Application.Common.Contracts.Database;

public interface IDocumentRepository : IRepository
{
    Task<Document> AddAsync(Document job, CancellationToken cancellationToken);
    Task<AcceptedDocument?> GetAcceptedDocument(Guid documentId, CancellationToken cancellationToken);

}