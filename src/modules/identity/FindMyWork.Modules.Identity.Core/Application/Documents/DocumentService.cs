using AutoMapper;
using FindMyWork.Modules.Identity.Core.Application.Common.Contracts.Database;
using FindMyWork.Modules.Identity.Core.Application.Documents.Contracts;
using FindMyWork.Modules.Identity.Core.Application.Documents.Models.RequestModels;
using FindMyWork.Modules.Identity.Core.Application.Documents.Models.ResponseModels;
using FindMyWork.Modules.Identity.Core.Domain.Entities;
using FindMyWork.Shared.Application.Models.ErrorModels;
using OneOf;

namespace FindMyWork.Modules.Identity.Core.Application.Documents;

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
        var document = _mapper.Map<Document>(request);

        var addedDocument = await _documentRepository.AddAsync(document, cancellationToken);

        return true;
    }

    public async Task<OneOf<AcceptedDocumentResponse, EntityNotFound>> AcceptDocumentAsync(
        Guid documentId,
        Guid signerId,
        CancellationToken cancellationToken)
    {
        var acceptedDocument = await _documentRepository.GetAcceptedDocument(documentId, cancellationToken);
        if (acceptedDocument is null)
        {
            return new EntityNotFound($"Document with id {documentId} not found");
        }

        acceptedDocument.Accepted = true;
        acceptedDocument.SignedAt = DateTimeOffset.UtcNow;
        acceptedDocument.SignedById = signerId;
        
        acceptedDocument.UpdatedAt = DateTimeOffset.UtcNow;
        acceptedDocument.Document.UpdatedAt = DateTimeOffset.UtcNow;

        return new AcceptedDocumentResponse();
    }
}