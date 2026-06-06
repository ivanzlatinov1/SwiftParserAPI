using Microsoft.Data.Sqlite;
using SwiftParser.Data.Entities;
using SwiftParser.Data.Interfaces;

namespace SwiftParser.Data.Repositories;

public sealed class LogRepository(IUnitOfWork unitOfWork) : ILogRepository
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddAsync(Log log)
    {
        string sql = """
             INSERT INTO Logs
                (Id, Message, TimeStamp)
              VALUES
                (@Id, @Message, @TimeStamp);
        """;

        await _unitOfWork.ExecuteAsync(sql,
                    new SqliteParameter("@Id", log.Id),
                    new SqliteParameter("@Message", log.Message),
                    new SqliteParameter("@TimeStamp", log.Timestamp));
    }

    public Task<IEnumerable<Log>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}