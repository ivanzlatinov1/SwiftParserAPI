using SwiftParser.Data.Entities;
using SwiftParser.Data.Interfaces;
using SwiftParser.DTOs;
using SwiftParser.Services.Interfaces;
using SwiftParser.Services.Mappers;

namespace SwiftParser.Services.Implementations;

public sealed class LogService(ILogRepository logRepository) : ILogService
{
    private readonly ILogRepository _logRepository = logRepository;
    public async Task<List<LogDTO>> QueryAllAsync()
    {
        IEnumerable<Log> logs = await _logRepository.GetAllAsync().ConfigureAwait(false);

        return [.. logs.Select(x => x.ToDTO())];
    }
}