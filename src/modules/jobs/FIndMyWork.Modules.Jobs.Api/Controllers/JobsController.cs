using FindMyWork.Modules.Jobs.Core.Application.Jobs.Contracts;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Models.ResponseModels;
using FindMyWork.Shared.Application.Models.ErrorModels;
using FindMyWork.Shared.Application.Models.ResponseModels;
using FindMyWork.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FIndMyWork.Modules.Jobs.Api.Controllers;

[ApiVersion("1.0")]
[Route(BaseApiPath + "/" + JobsModule.ModulePath)]
public class JobsController : BaseController
{
    private readonly IJobService _jobService;

    public JobsController(IJobService jobService)
    {
        _jobService = jobService;
    }

    /// <summary>
    /// Try to get job by id.
    /// </summary>
    /// <param name="id">Job id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JobResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseModel))]   
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var response = await _jobService.GetByIdAsync(id, cancellationToken);

        return response.Match<IActionResult>(
            Ok,
            notFound => NotFound(new ErrorResponseModel(nameof(EntityNotFound), notFound.Message)));
    }
    
    /// <summary>
    /// Post job as draft.
    /// </summary>
    /// <param name="employerId">Owner of the job</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(JobResponse))]
    public async Task<IActionResult> PostJobAsync([FromQuery]Guid employerId, CancellationToken cancellationToken)
    {
        var response = await _jobService.PostJobAsync(employerId, cancellationToken);

        return Created($"api/v1.0/{JobsModule.ModulePath }/{response.Id}", response);
    }
}