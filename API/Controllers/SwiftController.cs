using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SwiftParser.Application.Interfaces;
using SwiftParser.Application.DTOs;
using static SwiftParser.Shared.MessageConstants.SwiftMessages;
using static SwiftParser.Shared.ErrorConstants;

namespace SwiftParser.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[SwaggerTag(SwiftApiTag)]
public class SwiftController(ISwiftParserService swiftParserService, ILogger<SwiftController> logger) : ControllerBase
{
    private readonly ILogger<SwiftController> _logger = logger;
    private readonly ISwiftParserService _swiftParserService = swiftParserService;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = GetAllMessagesSummary, Description = GetAllMessagesDescription)]
    public async Task<ActionResult<List<SwiftMessageDTO>>> GetAllMessages()
    {
        List<SwiftMessageDTO> messages = await _swiftParserService.QueryAllAsync();

        if (messages.Count == 0)
        {
            _logger.LogInformation(DatabaseEmpty);
            return Ok(messages);
        }

        return Ok(messages);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = UploadMethodSummary, Description = UploadMethodDescription)]
    public async Task<ActionResult<string>> Upload([FromForm] UploadRequest req)
    {
        IFormFile? file = req.File;
        if (file is null || file.Length == 0)
        {
            _logger.LogWarning(NoFileProvided);
            return BadRequest(NoFileProvided);
        }

        if (!file.FileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogWarning(InvalidFileType);
            return BadRequest(InvalidFileType);
        }

        _logger.LogInformation(string.Format(MessageReceived, file.FileName, (float)file.Length / 1000));

        string swiftMessage;
        try
        {
            swiftMessage = await _swiftParserService.ParseMessageAsync(file);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ParsingFailed);
            return BadRequest(ParsingFailed);
        }

        _logger.LogInformation(ParsingComplete);

        return Ok(swiftMessage);
    }
}