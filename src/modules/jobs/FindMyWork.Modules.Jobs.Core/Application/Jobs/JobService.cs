using AutoMapper;
using OneOf;
using FindMyWork.Modules.Jobs.Core.Application.Common.Contracts.Database;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Contracts;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.RequestModels;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.ResponseModels;
using FindMyWork.Modules.Jobs.Core.Domain.Entities;
using FindMyWork.Modules.Jobs.Core.Domain.Enums;
using FindMyWork.Shared.Application.Models.ErrorModels;
using FindMyWork.Shared.Application.Models.ResponseModels;
using FindMyWork.Shared.Application.Models.SuccessModels;
using FindMyWork.Shared.Infrastructure.Validators;

namespace FindMyWork.Modules.Jobs.Core.Application.Jobs;

internal class JobService : IJobService
{
    private readonly IMapper _mapper;
    private readonly IJobRepository _jobRepository;
    private readonly IPaginationHelper _paginationHelper;
    private readonly IValidationFactory _validationFactory;

    public JobService(
        IMapper mapper,
        IJobRepository jobRepository, 
        IPaginationHelper paginationHelper, 
        IValidationFactory validationFactory)
    {
        _mapper = mapper;
        _jobRepository = jobRepository;
        _paginationHelper = paginationHelper;
        _validationFactory = validationFactory;
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

    public async Task<OneOf<JobResponse, EntityNotValid>> PostJobAsync(
        Guid employerId, 
        AddJobRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validationFactory.ValidateAsync(request);
        return await validationResult.Match<Task<OneOf<JobResponse, EntityNotValid>>>(
            async success =>
            {
                var jobToAdd = _mapper.Map<Job>((request, employerId));

                var addedJob = await _jobRepository.AddAsync(jobToAdd, cancellationToken);
                await _jobRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                var response = _mapper.Map<JobResponse>(addedJob);

                return response;
            }, entityNotValid => Task.FromResult<OneOf<JobResponse, EntityNotValid>>(entityNotValid));
    }

    public async Task<PaginatedResponse<IEnumerable<JobResponse>?>> GetByFilter(
        JobFilterRequest request,
        string route,
        CancellationToken cancellationToken)
    {
        var jobs = await _jobRepository.GetPaginatedByCategoryAsync(
            request.CategoryId,
            request.Page, 
            request.Take, 
            cancellationToken);
        var totalCount = await _jobRepository.CountByCategoryAsync(request.CategoryId, cancellationToken);

        var jobsResponse = _mapper.Map<IList<JobResponse>>(jobs);
        var response = _paginationHelper.CreatePagedResponse(
            jobsResponse,
            request.Page,
            request.Take,
            totalCount,
            route);

        return response;
    }

    public async Task<OneOf<OperationSuccess, EntityNotFound>> SoftDeleteAsync(
        Guid id, 
        Guid userId,
        CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetByIdAsync(id, cancellationToken);
        if (job is null)
        {
            return new EntityNotFound($"Job with {id} not found");
        }

        var lastStatus = job.JobStatusInfos
            .OrderBy(x => x.CreatedAt)
            .Last();
        
        var jobStatusInfo = new JobStatusInfo
        {
            JobId = id,
            CurrentStatus = JobStatus.Archived,
            OldStatus = lastStatus.CurrentStatus,
            InitiatorId = userId
        };

        job.Deleted = true;
        job.JobInformation.Deleted = true;
        job.Status = JobStatus.Archived;
        job.UpdatedAt = DateTimeOffset.UtcNow;
        job.JobInformation.UpdatedAt = DateTimeOffset.UtcNow;
        
        job.JobStatusInfos.Add(jobStatusInfo);

        _jobRepository.Update(job);
        await _jobRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new OperationSuccess();
    }
}