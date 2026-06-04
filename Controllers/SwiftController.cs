using Microsoft.AspNetCore.Mvc;
using NLog;
using Swashbuckle.AspNetCore.Annotations;
using SwiftParser.DTOs;
using static SwiftParser.Shared.MessageConstants;

namespace SwiftParser.Controllers;

[ApiController]
[Route("api/[controller]")]
[SwaggerTag("Endpoints for uploading and processing Swift MT799 financial messages")]
public class SwiftController : ControllerBase
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private static readonly SwiftParserService _parserService = new();

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = UploadMethodSummary, Description = UploadMethodDescription)]
    public async Task<ActionResult> Upload([FromForm] UploadRequest req)
    {
        IFormFile? file = req.File;
        if (file is null || file.Length == 0)
        {
            _logger.Warn("Upload rejected — no file provided!");
            return BadRequest("No file provided!");
        }

        if (!file.FileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
        {
            _logger.Warn($"Upload rejected — invalid file type: {file.FileName}");
            return BadRequest("Only .txt files are accepted!");
        }

        _logger.Info($"Upload request received. File info: {file.FileName} with size {(float)file.Length / 1000} KB");

        string messageContent;
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            messageContent = await reader.ReadToEndAsync();
        }

        bool isParsed = _parserService.ParseSwiftMessage(messageContent);
        if (!isParsed)
        {
            _logger.Warn("Failed to parse the uploaded Swift message.");
            return BadRequest("Failed to parse the uploaded Swift message.");
        }

        _logger.Info("Swift message parsed successfully!");
        return Ok();
    }
}