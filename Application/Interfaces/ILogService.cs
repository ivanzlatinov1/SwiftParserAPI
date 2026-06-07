using SwiftParser.Application.DTOs;

namespace SwiftParser.Application.Interfaces;

public interface ILogService
{
    Task<List<LogDTO>> QueryAllAsync();
    Task<LogDTO?> GetByIdAsync(Guid logId);
    Task<bool> DeleteAsync(Guid logId);
}