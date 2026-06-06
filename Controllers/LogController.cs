using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SwiftParser.DTOs;
using static SwiftParser.Shared.MessageConstants.LogMessages;
using SwiftParser.Services.Interfaces;

namespace SwiftParser.Controllers;

[ApiController]
[Route("api/[controller]")]
[SwaggerTag(LogApiTag)]
public class LogsController(ILogService logService, ILogger<LogsController> logger) : ControllerBase
{
    private readonly ILogger<LogsController> _logger = logger;
    private readonly ILogService _logService = logService;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = GetAllLogsSummary, Description = GetAllLogsDescription)]
    public async Task<ActionResult<List<LogDTO>>> GetAllMessages()
    {
        List<LogDTO> logs = await _logService.QueryAllAsync();

        if (logs.Count == 0)
        {
            _logger.LogInformation(DatabaseEmpty);
            return Ok(logs);
        }

        _logger.LogInformation(SuccessfulOperation);

        return Ok(logs);
    }
}