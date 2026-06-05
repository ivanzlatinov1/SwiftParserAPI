using SwiftParser.Data.Entities;

namespace SwiftParser.Data.Interfaces;

public interface ISwiftRepository
{
    Task AddAsync(SwiftMessage message);

    Task<IEnumerable<SwiftMessage>> GetAllAsync();

    Task<SwiftMessage?> GetByIdAsync(int id);

    Task DeleteAsync(int id);
}