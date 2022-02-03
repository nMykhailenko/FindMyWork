using AutoMapper;
using FindMyWork.Modules.Users.Core.Application.Common.Contracts.Database;
using FindMyWork.Modules.Users.Core.Application.Documents.Contracts;
using FindMyWork.Modules.Users.Core.Application.Documents.Models.RequestModels;
using FindMyWork.Modules.Users.Core.Domain.Entities;

namespace FindMyWork.Modules.Users.Core.Application.Documents;

public class DocumentService : IDocumentService
{
    private readonly IMapper _mapper;
    private readonly IDocumentRepository _documentRepository;

    public DocumentService(
        IMapper mapper, 
        IDocumentRepository documentRepository)
    {
        _mapper = mapper;
        _documentRepository = documentRepository;
    }

    public async Task<bool> AssignDocumentAsync(
        AssignDocumentRequest request, 
        Guid userId, 
        CancellationToken cancellationToken)
    {
        var document = _mapper.Map<Document>((request, userId));

        var addedDocument = await _documentRepository.AddAsync(document, cancellationToken);

        return true;
    }

    public async Task<bool> AcceptDocumentAsync(
        Guid documentId,
        Guid signerId,
        CancellationToken cancellationToken)
    {
        var acceptedDocument = await _documentRepository.GetAcceptedDocument(documentId, cancellationToken);
        if (acceptedDocument is null)
        {
            return false;
        }

        acceptedDocument.Accepted = true;
        acceptedDocument.SignedAt = DateTimeOffset.UtcNow;
        acceptedDocument.SignedById = signerId;
        
        acceptedDocument.UpdatedAt = DateTimeOffset.UtcNow;
        acceptedDocument.Document.UpdatedAt = DateTimeOffset.UtcNow;

        return true;
    }
}