using Microsoft.Data.Sqlite;
using SwiftParser.Data.Interfaces;

namespace SwiftParser.Repositories;

public sealed class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly SqliteConnection _connection;
    private SqliteTransaction? _transaction;

    public SqliteConnection Connection => _connection;
    public SqliteTransaction? Transaction => _transaction;

    public UnitOfWork(SqliteConnection connection)
    {
        _connection = connection;
        _connection.Open();
    }

    public void BeginTransaction()
    {
        _transaction = _connection.BeginTransaction();
    }

    public async Task ExecuteAsync(string sql, params SqliteParameter[] parameters)
    {
        await using var cmd = _connection.CreateCommand();
        cmd.CommandText = sql;

        if (_transaction != null)
            cmd.Transaction = _transaction;

        if (parameters?.Length > 0)
            cmd.Parameters.AddRange(parameters);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<List<T>> QueryAsync<T>(string sql, Func<SqliteDataReader, T> map, params SqliteParameter[] parameters)
    {
        await using var cmd = _connection.CreateCommand();

        cmd.CommandText = sql;

        if (_transaction != null)
            cmd.Transaction = _transaction;

        if (parameters?.Length > 0)
            cmd.Parameters.AddRange(parameters);

        await using var reader = await cmd.ExecuteReaderAsync();

        List<T> results = [];

        while (await reader.ReadAsync())
        {
            results.Add(map(reader));
        }

        return results;
    }

    public void Commit()
    {
        _transaction?.Commit();
        _transaction?.Dispose();
        _transaction = null;
    }

    public void Rollback()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
        _transaction = null;
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _connection.Dispose();
    }
}