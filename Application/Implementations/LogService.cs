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
}