namespace SwiftParser.Services.Interfaces;

public interface ISwiftParserService
{
    Task<string> ParseSwiftMessage(IFormFile file);
}