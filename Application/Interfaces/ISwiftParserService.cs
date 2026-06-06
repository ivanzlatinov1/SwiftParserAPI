using SwiftParser.Application.DTOs;

namespace SwiftParser.Application.Interfaces;

public interface ISwiftParserService
{
    Task<string> ParseMessageAsync(IFormFile file);
    Task<List<SwiftMessageDTO>> QueryAllAsync();
}