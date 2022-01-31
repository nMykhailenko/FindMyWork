﻿using AutoMapper;
using OneOf;
using FindMyWork.Modules.Jobs.Core.Application.Common.Contracts.Database;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Contracts;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.RequestModels;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.ResponseModels;
using FindMyWork.Modules.Jobs.Core.Domain.Entities;
using FindMyWork.Shared.Application.Models.ErrorModels;

namespace FindMyWork.Modules.Jobs.Core.Application.Jobs;

internal class JobService : IJobService
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

    public async Task<JobResponse> PostJobAsync(
        Guid employerId, 
        AddJobRequest request,
        CancellationToken cancellationToken)
    {
        var jobToAdd = _mapper.Map<Job>((request, employerId));

        var addedJob = await _jobRepository.AddAsync(jobToAdd, cancellationToken);
        await _jobRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        var response = _mapper.Map<JobResponse>(addedJob);

        return response;
    }
}