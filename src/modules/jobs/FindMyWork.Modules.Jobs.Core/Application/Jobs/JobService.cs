using AutoMapper;
using OneOf;
using FindMyWork.Modules.Jobs.Core.Application.Common.Contracts.Database;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Contracts;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.ResponseModels;
using FindMyWork.Shared.Application.Models.ErrorModels;

namespace FindMyWork.Modules.Jobs.Core.Application.Jobs;

public class JobService : IJobService
{
    private readonly IMapper _mapper;
    private readonly IJobRepository _jobRepository;

    public JobService(
        IMapper mapper,
        IJobRepository jobRepository)
    {
        _mapper = mapper;
        _jobRepository = jobRepository;
    }

    public async Task<OneOf<JobResponse, EntityNotFound>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetByIdAsync(id, cancellationToken);
        if (job is null)
        {
            return new EntityNotFound($"Job with {id} not found");
        }

        var response = _mapper.Map<JobResponse>(job);
        return response;
    }
}