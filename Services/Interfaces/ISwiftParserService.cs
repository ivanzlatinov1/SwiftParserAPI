using SwiftParser.DTOs;

namespace SwiftParser.Services.Interfaces;

public interface ISwiftParserService
{
    Task<string> ParseMessageAsync(IFormFile file);
    Task<List<SwiftMessageDTO>> QueryAllAsync();
}