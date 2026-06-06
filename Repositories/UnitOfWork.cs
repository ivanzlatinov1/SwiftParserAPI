using System.Data;
using Microsoft.Data.Sqlite;
using SwiftParser.Data.Interfaces;

namespace SwiftParser.Repositories;

public sealed class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly SqliteConnection _connection;
    private SqliteTransaction? _transaction;

    public SqliteConnection Connection => _connection;
    public SqliteTransaction? Transaction => _transaction;

    public UnitOfWork(IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found!");
        _connection = new SqliteConnection(connectionString);
        _connection.Open();
    }

    public void BeginTransaction()
    {
        _transaction = _connection.BeginTransaction();
    }

    public void EnsureDatabaseCreated()
    {
        string sql = """
            CREATE TABLE IF NOT EXISTS SwiftMessages (
                Id TEXT PRIMARY KEY,
                TransactionReferenceNumber TEXT,
                BankOperationCode TEXT,
                ValueDate TEXT,
                CurrencyCode TEXT,
                SettlementAmount REAL,
                InstructedAmount REAL,
                OrderingCustomer TEXT,
                BeneficiaryBank TEXT,
                Beneficiary TEXT,
                PaymentReference TEXT,
                DetailsOfCharges TEXT,
                SenderBic TEXT,
                ReceiverBic TEXT
            );
            """;

        using var command = _connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }

    public async Task ExecuteAsync(string sql, params SqliteParameter[] parameters)
    {
        await using var command = _connection.CreateCommand();
        command.CommandText = sql;

        if (_transaction != null)
            command.Transaction = _transaction;

        if (parameters?.Length > 0)
            command.Parameters.AddRange(parameters);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<List<T>> QueryAsync<T>(string sql, Func<SqliteDataReader, T> map, params SqliteParameter[] parameters)
    {
        await using var command = _connection.CreateCommand();

        command.CommandText = sql;

        if (_transaction != null)
            command.Transaction = _transaction;

        if (parameters?.Length > 0)
            command.Parameters.AddRange(parameters);

        await using var reader = await command.ExecuteReaderAsync();

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