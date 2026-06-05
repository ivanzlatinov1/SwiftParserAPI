using NLog;
using SwiftParser.Data.Interfaces;
using SwiftParser.Services.Interfaces;

namespace SwiftParser.Services.Implementations;

public class SwiftParserService(ISwiftRepository swiftRepository) : ISwiftParserService
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly ISwiftRepository _swiftRepository = swiftRepository;

    public async Task<string> ParseSwiftMessage(IFormFile file)
    {
        string messageContent;
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            messageContent = await reader.ReadToEndAsync();
        }

        if (string.IsNullOrWhiteSpace(messageContent))
        {
            _logger.Warn("The uploaded file is empty.");
            throw new ArgumentException("The uploaded file is empty.");
        }

        // TODO: Implement actual parsing logic here. For now, we just return the raw content.

        return messageContent;
    }
}