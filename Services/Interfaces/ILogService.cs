using SwiftParser.DTOs;

namespace SwiftParser.Services.Interfaces;

public interface ILogService
{
    Task<List<LogDTO>> QueryAllAsync();
}