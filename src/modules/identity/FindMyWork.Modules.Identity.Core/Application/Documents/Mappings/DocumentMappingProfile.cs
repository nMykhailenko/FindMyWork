using AutoMapper;
using FindMyWork.Modules.Users.Core.Application.Documents.Models.RequestModels;
using FindMyWork.Modules.Users.Core.Domain.Entities;

namespace FindMyWork.Modules.Users.Core.Application.Documents.Mappings;

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