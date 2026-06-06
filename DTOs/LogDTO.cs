namespace SwiftParser.DTOs;

public sealed class LogDTO
{
    public Guid Id { get; init; }
    public string Message { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}