using FindMyWork.Modules.Identity.Core.Application.Common.Contracts.Database;
using FindMyWork.Modules.Identity.Core.Domain.Entities;
using FindMyWork.Modules.Identity.Core.Infrastructure.Persistence;
using FindMyWork.Shared.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FindMyWork.Modules.Identity.Core.Infrastructure.Repositories;

public class DocumentRepository : Repository, IDocumentRepository
{
    private IdentityDbContext DbContext => (UnitOfWork as IdentityDbContext)!;

    public DocumentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Document> AddAsync(Document document, CancellationToken cancellationToken)
    {
        var addedDocument = await DbContext.Documents.AddAsync(document, cancellationToken);
        return addedDocument.Entity;
    }

    public Task<AcceptedDocument?> GetAcceptedDocument(Guid documentId, CancellationToken cancellationToken)
    {
        return DbContext.AcceptedDocuments
            .FirstOrDefaultAsync(x => x.DocumentId == documentId, cancellationToken);
    }
}