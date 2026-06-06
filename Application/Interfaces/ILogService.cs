using SwiftParser.Application.DTOs;

namespace SwiftParser.Application.Interfaces;

public interface ILogService
{
    Task<List<LogDTO>> QueryAllAsync();
}