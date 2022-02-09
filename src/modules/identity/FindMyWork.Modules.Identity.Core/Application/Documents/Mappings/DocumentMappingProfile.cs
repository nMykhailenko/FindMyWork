using AutoMapper;
using FindMyWork.Modules.Identity.Core.Application.Documents.Models.RequestModels;
using FindMyWork.Modules.Identity.Core.Domain.Entities;

namespace FindMyWork.Modules.Identity.Core.Application.Documents.Mappings;

public class DocumentMappingProfile : Profile
{
    public DocumentMappingProfile()
    {
        CreateMap<AssignDocumentRequest, Document>()
            .ForMember(
                dest => dest.Url,
                options => options.MapFrom(src => src.Url))
            .ForMember(
                dest => dest.Type,
                options => options.MapFrom(src => src.Type))
            .ForMember(
                dest => dest.CreatedAt, 
                options => options.MapFrom(src => DateTimeOffset.UtcNow))
            .ForMember(
                dest => dest.AcceptedDocument, 
                options => options.MapFrom(src => new AcceptedDocument
                {
                    Accepted = false, CreatedAt = DateTimeOffset.UtcNow,
                }));
    }
}