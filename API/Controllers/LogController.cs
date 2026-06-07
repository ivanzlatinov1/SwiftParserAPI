using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SwiftParser.Application.Interfaces;
using SwiftParser.Application.DTOs;
using static SwiftParser.Shared.MessageConstants.LogMessages;
using static SwiftParser.Shared.ErrorConstants.LogMessages;

namespace SwiftParser.API.Controllers;

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
    public async Task<ActionResult<List<LogDTO>>> GetAllLogs()
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

    [HttpGet("{logId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = GetLogByIdSummary, Description = GetLogByIdDescription)]
    public async Task<ActionResult<LogDTO>> GetLogById(Guid logId)
    {
        LogDTO? log = await _logService.GetByIdAsync(logId);

        if (log is null)
        {
            _logger.LogError(string.Format(LogNotFound, logId));
            return NotFound(string.Format(LogNotFound, logId));
        }

        return Ok(log);
    }

    [HttpDelete("{logId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = DeleteLogSummary, Description = DeleteLogDescription)]
    public async Task<ActionResult> DeleteLog(Guid logId)
    {
        bool isDeleted = await _logService.DeleteAsync(logId);

        if (!isDeleted)
        {
            _logger.LogError(string.Format(InvalidOperation, logId));
            return NotFound(InvalidOperation);
        }

        return Ok(SuccessfulOperation);
    }
}