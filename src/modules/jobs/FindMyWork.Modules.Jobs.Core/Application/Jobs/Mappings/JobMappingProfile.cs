using AutoMapper;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.ResponseModels;
using FindMyWork.Modules.Jobs.Core.Domain.Entities;

namespace FindMyWork.Modules.Jobs.Core.Application.Jobs.Mappings;

public class JobMappingProfile : Profile
{
    public JobMappingProfile()
    {
        CreateMap<Job, JobResponse>(MemberList.Destination);
        CreateMap<JobInformation, JobInformationResponse>(MemberList.Destination);
        CreateMap<Address, AddressResponse>(MemberList.Destination);
    }
}