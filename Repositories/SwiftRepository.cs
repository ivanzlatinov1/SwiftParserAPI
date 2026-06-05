using SwiftParser.Data.Entities;
using SwiftParser.Data.Interfaces;

namespace SwiftParser.Repositories;

public class SwiftRepository(IUnitOfWork unitOfWork) : ISwiftRepository
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddAsync(SwiftMessage message)
    {
        // string sql = "INSERT INTO SwiftMessages (Content, UploadedAt) VALUES (@Content, @UploadedAt)";
        // to do parameters
        // to do execute query
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<SwiftMessage>> GetAllAsync()
    {
        string sql = "SELECT * FROM SwiftMessages";
        return await _unitOfWork.QueryAsync(sql, reader => new SwiftMessage
        {
            Id = reader.GetInt32(0),
        });
    }

    public Task<SwiftMessage?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}