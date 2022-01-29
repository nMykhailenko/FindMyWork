using AutoMapper;
using OneOf;
using FindMyWork.Modules.Jobs.Core.Application.Common.Contracts.Database;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Contracts;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.ResponseModels;
using FindMyWork.Modules.Jobs.Core.Domain.Entities;
using FindMyWork.Modules.Jobs.Core.Domain.Enums;
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

    public async Task<JobResponse> PostJobAsync(Guid employerId, CancellationToken cancellationToken)
    {
        var job = new Job
        {
            EmployerId = employerId,
            Status = JobStatus.Draft,
            JobStatusInfos = new List<JobStatusInfo>
            {
                new() {CurrentStatus = JobStatus.Draft, OldStatus = null, InitiatorId = employerId}
            }
        };

        var addedJob = await _jobRepository.AddAsync(job, cancellationToken);
        await _jobRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        var response = _mapper.Map<JobResponse>(addedJob);

        return response;
    }
}