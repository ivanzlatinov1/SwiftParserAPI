using Microsoft.AspNetCore.Mvc;
using NLog;
using Swashbuckle.AspNetCore.Annotations;
using SwiftParser.DTOs;
using SwiftParser.Services.Implementations;
using static SwiftParser.Shared.MessageConstants;
using static SwiftParser.Shared.ErrorConstants;

namespace SwiftParser.Controllers;

[ApiController]
[Route("api/[controller]")]
[SwaggerTag(SwiftApiTag)]
public class SwiftController(SwiftParserService swiftParserService) : ControllerBase
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly SwiftParserService _swiftParserService = swiftParserService;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = UploadMethodSummary, Description = UploadMethodDescription)]
    public async Task<ActionResult<string>> Upload([FromForm] UploadRequest req)
    {
        IFormFile? file = req.File;
        if (file is null || file.Length == 0)
        {
            _logger.Warn(NoFileProvided);
            return BadRequest(NoFileProvided);
        }

        if (!file.FileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
        {
            _logger.Warn(InvalidFileType);
            return BadRequest(InvalidFileType);
        }

        _logger.Info($"Upload request received. File info: {file.FileName} with size {(float)file.Length / 1000} KB");

        string swiftMessage = await _swiftParserService.ParseSwiftMessage(file);

        _logger.Info(ParsingComplete);

        return Ok(swiftMessage);
    }
}