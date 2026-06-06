using SwiftParser.Data.Entities;

namespace SwiftParser.Data.Interfaces;

public interface ILogRepository
{
    Task<IEnumerable<Log>> GetAllAsync();
    Task AddAsync(Log log);
    Task DeleteAsync(Guid id);
}