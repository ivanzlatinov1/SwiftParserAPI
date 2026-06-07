using SwiftParser.Application.Interfaces;
using SwiftParser.Application.Mappers;
using SwiftParser.Domain.Entities;
using SwiftParser.Repositories.Interfaces;
using SwiftParser.Application.DTOs;

namespace SwiftParser.Application.Implementations;

public sealed class LogService(ILogRepository logRepository) : ILogService
{
    private readonly ILogRepository _logRepository = logRepository;

    public async Task<List<LogDTO>> QueryAllAsync()
    {
        IEnumerable<Log> logs = await _logRepository.GetAllAsync().ConfigureAwait(false);

        return [.. logs.Select(x => x.ToDTO())];
    }

    public async Task<LogDTO?> GetByIdAsync(Guid logId)
    {
        Log? log = await _logRepository.GetByIdAsync(logId);

        if (log is null)
        {
            return null;
        }

        return log.ToDTO();
    }

    public async Task<bool> DeleteAsync(Guid logId)
    {
        Log? log = await _logRepository.GetByIdAsync(logId);

        if (log is null)
        {
            await _logRepository.AddAsync(new()
            {
                Message = $"Attempted to delete log with wrong id! {logId}",
                Timestamp = DateTime.UtcNow
            });
            return false;
        }

        await _logRepository.DeleteAsync(logId);
        return true;
    }
}