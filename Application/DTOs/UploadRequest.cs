namespace SwiftParser.Application.DTOs;

public class UploadRequest
{
    public required IFormFile File { get; init; }
}