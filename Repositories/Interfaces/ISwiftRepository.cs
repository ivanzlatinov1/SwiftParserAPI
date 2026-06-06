using SwiftParser.Domain.Entities;

namespace SwiftParser.Repositories.Interfaces;

public interface ISwiftRepository
{
    Task AddAsync(SwiftMessage message);

    Task<IEnumerable<SwiftMessage>> GetAllAsync();

    Task<SwiftMessage?> GetByIdAsync(Guid id);

    Task DeleteAsync(Guid id);
}