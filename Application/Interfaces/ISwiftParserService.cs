using SwiftParser.Application.DTOs;

namespace SwiftParser.Application.Interfaces;

public interface ISwiftParserService
{
    Task<SwiftMessageDTO> ParseMessageAsync(IFormFile file);
    Task<List<SwiftMessageDTO>> QueryAllAsync();
    Task<SwiftMessageDTO?> GetByIdAsync(Guid id);
    Task<bool> DeleteMessageAsync(Guid id);
}