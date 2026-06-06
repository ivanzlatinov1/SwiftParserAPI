using Microsoft.Data.Sqlite;

namespace SwiftParser.Data.Interfaces;

public interface IUnitOfWork
{
    Task ExecuteAsync(string sql, params SqliteParameter[] parameters);
    Task<List<T>> QueryAsync<T>(string sql, Func<SqliteDataReader, T> map, params SqliteParameter[] parameters);

    void EnsureDatabaseCreated();
    void BeginTransaction();
    void Commit();
    void Rollback();
    void Dispose();
}