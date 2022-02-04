using AutoMapper;
using FindMyWork.Modules.Files.Core.Application.Files.Models.RequestModels;
using FindMyWork.Modules.Files.Core.Application.Files.Models.ResponseModels;
using FindMyWork.Shared.Application.Enums;
using File = FindMyWork.Modules.Files.Core.Domain.Entities.File;

namespace FindMyWork.Modules.Files.Core.Application.Files.Mappings;

public class FileMappingProfile : Profile
{
    public FileMappingProfile()
    {
        CreateMap<(UploadFileRequest request, Guid userId, UserType userType, string  fileName), File>()
            .ForMember(
                dest => dest.Type,
                options => options.MapFrom(src => src.request.Type))
            .ForMember(
                dest => dest.Name,
                options => options.MapFrom(src => src.fileName))
            .ForMember(
                dest => dest.CreatedByUserId,
                options => options.MapFrom(src => src.userId))
            .ForMember(
                dest => dest.CreatedByUserType,
                options => options.MapFrom(src => src.userType));

        CreateMap<(File file, string url), UploadFileResponse>()
            .ForMember(
                dest => dest.Id,
                options => options.MapFrom(src => src.file.Id))
            .ForMember(
                dest => dest.Type,
                options => options.MapFrom(src => src.file.Type))
            .ForMember(
                dest => dest.FileName,
                options => options.MapFrom(src => src.file.Name))
            .ForMember(
                dest => dest.Url,
                options => options.MapFrom(src => src.url));
    }
}