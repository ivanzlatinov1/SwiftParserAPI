using SwiftParser.DTOs;

namespace SwiftParser.Services.Interfaces;

public interface ISwiftParserService
{
    Task<string> ParseSwiftMessage(IFormFile file);
    Task<List<SwiftMessageDTO>> GetAllMessagesAsync();
}