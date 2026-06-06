namespace SwiftParser.Domain.Entities;

public sealed class Log
{
    public Log()
    {
        Id = Guid.CreateVersion7();
    }

    public Guid Id { get; init; }
    public string Message { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}