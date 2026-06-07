using SwiftParser.Domain.Entities;

namespace SwiftParser.Repositories.Interfaces;

public interface ILogRepository
{
    Task<IEnumerable<Log>> GetAllAsync();
    Task AddAsync(Log log);
    Task<Log?> GetByIdAsync(Guid id);
    Task DeleteAsync(Guid id);
}