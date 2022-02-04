using FindMyWork.Modules.Files.Core.Application.Files.Contracts;
using FindMyWork.Modules.Files.Core.Application.Files.Models.RequestModels;
using FindMyWork.Modules.Files.Core.Application.Files.Models.ResponseModels;
using FindMyWork.Shared.Application.Models.ErrorModels;
using FindMyWork.Shared.Application.Models.ResponseModels;
using FindMyWork.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindMyWork.Modules.Files.Api.Controllers;

[ApiVersion("1.0")]
[Route(BaseApiPath + "/" + FilesModule.ModulePath)]
public class FilesController : BaseController
{
    private readonly IFileService _fileService;

    public FilesController(IFileService fileService)
    {
        _fileService = fileService;
    }
    
    [HttpGet("{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UploadFileResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]   
    public async Task<IActionResult> GetFileAsync(
        [FromRoute]Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _fileService.GetFileAsync(
            id,
            default,
            cancellationToken);

        return result.Match<IActionResult>(
            Ok, 
            notFound => NotFound(new ErrorResponse(nameof(EntityNotFound), notFound.Message)),
            BadRequest);
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UploadFileResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]   
    public async Task<IActionResult> UploadFileAsync(
        [FromForm] UploadFileRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _fileService.UploadFileAsync(
            request,
            default,
            default,
            cancellationToken);

        return result.Match<IActionResult>(Ok, BadRequest);
    }
}