using AutoMapper;
using FindMyWork.Modules.Files.Core.Application.Files.Models.RequestModels;
using FindMyWork.Modules.Files.Core.Application.Files.Models.ResponseModels;
using FindMyWork.Shared.Application.Enums;
using FindMyWork.Shared.Infrastructure.Extensions;
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
            .MapRecordMember(
                dest => dest.Id,
                src => src.file.Id)
            .MapRecordMember(
                dest => dest.Type,
                src => src.file.Type)
            .MapRecordMember(
                dest => dest.FileName,
                src => src.file.Name)
            .MapRecordMember(
                dest => dest.Url,
                src => src.url);
    }
}