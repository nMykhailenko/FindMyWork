using AutoMapper;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.RequestModels;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.ResponseModels;
using FindMyWork.Modules.Jobs.Core.Domain.Entities;
using FindMyWork.Modules.Jobs.Core.Domain.Enums;

namespace FindMyWork.Modules.Jobs.Core.Application.Jobs.Mappings;

public class JobMappingProfile : Profile
{
    public JobMappingProfile()
    {
        CreateMap<Job, JobResponse>(MemberList.Destination);
        CreateMap<JobInformation, JobInformationResponse>(MemberList.Destination);
        CreateMap<Address, AddressResponse>(MemberList.Destination);

        CreateMap<(AddJobRequest request, Guid employerId), Job>()
            .ForMember(
                dest => dest.JobInformation.CategoryId,
                options => options.MapFrom(src => src.request.CategoryId))
            .ForMember(
                dest => dest.JobInformation.ContactPersonId,
                options => options.MapFrom(src => src.request.ContactPersonId))
            .ForMember(
                dest => dest.JobInformation.Title,
                options => options.MapFrom(src => src.request.Title))
            .ForMember(
                dest => dest.JobInformation.Description,
                options => options.MapFrom(src => src.request.Description))
            .ForMember(
                dest => dest.JobInformation.NumberEmployeesToHire,
                options => options.MapFrom(src => src.request.NumberEmployeesToHire))
            .ForMember(
                dest => dest.JobInformation.CategoryId,
                options => options.MapFrom(src => src.request.SalaryPerEmployee))
            .ForMember(
                dest => dest.JobInformation.CategoryId,
                options => options.MapFrom(src => src.request.TotalHoursToWork))
            .ForMember(
                dest => dest.JobInformation.CategoryId,
                options => options.MapFrom(src => src.request.StartsOn))
            .ForMember(
                dest => dest.JobInformation.CategoryId,
                options => options.MapFrom(src => src.request.EndsOn))
            .ForMember(
                dest => dest.JobInformation.Location,
                options => options.MapFrom(src => src.request.Location))
            .ForMember(
                dest => dest.Status,
                options => options.MapFrom(src => JobStatus.Draft))
            .ForMember(
                dest => dest.JobStatusInfos,
                options
                    => options.MapFrom(src => new List<JobStatusInfo>
                    {
                        new() { CurrentStatus = JobStatus.Draft, OldStatus = null, InitiatorId = src.employerId }
                    }))
            .ForMember(
                dest => dest.EmployerId, 
                options => options.MapFrom(src => src.employerId));

        CreateMap<AddAddressRequest, Address>(MemberList.Destination);
    }
}