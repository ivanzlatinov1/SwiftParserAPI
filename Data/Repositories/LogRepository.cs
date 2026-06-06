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

    public async Task<IEnumerable<Log>> GetAllAsync()
    {
        string sql = "SELECT * FROM Logs";

        IEnumerable<Log> logs = await _unitOfWork.QueryAsync(sql, reader => new Log
        {
            Id = reader.GetGuid(reader.GetOrdinal("Id")),
            Message = reader.GetString(reader.GetOrdinal("Message")),
            Timestamp = reader.GetDateTime(reader.GetOrdinal("Timestamp"))
        });

        return logs;
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}