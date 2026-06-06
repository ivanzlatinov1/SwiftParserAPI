using SwiftParser.Data.Entities;
using SwiftParser.Data.Interfaces;
using Microsoft.Data.Sqlite;

namespace SwiftParser.Repositories;

public class SwiftRepository(IUnitOfWork unitOfWork) : ISwiftRepository
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task AddAsync(SwiftMessage message)
    {
        string sql = """
            INSERT INTO SwiftMessages 
                    (Id, TransactionReferenceNumber, BankOperationCode, ValueDate,
                    CurrencyCode, SettlementAmount, InstructedAmount, OrderingCustomer, BeneficiaryBank, Beneficiary,
                    PaymentReference, DetailsOfCharges, SenderBic, ReceiverBic)
            VALUES 
                    (@Id, @TransactionReferenceNumber, @BankOperationCode, @ValueDate, @CurrencyCode,
                    @SettlementAmount, @InstructedAmount, @OrderingCustomer, @BeneficiaryBank, @Beneficiary, @PaymentReference,
                    @DetailsOfCharges, @SenderBic, @ReceiverBic);
            """;

        await _unitOfWork.ExecuteAsync(sql,
            new SqliteParameter("@Id", message.Id),
            new SqliteParameter("@TransactionReferenceNumber", message.TransactionReferenceNumber),
            new SqliteParameter("@BankOperationCode", message.BankOperationCode),
            new SqliteParameter("@ValueDate", message.ValueDate),
            new SqliteParameter("@CurrencyCode", message.CurrencyCode),
            new SqliteParameter("@SettlementAmount", message.SettlementAmount),
            new SqliteParameter("@InstructedAmount", message.InstructedAmount),
            new SqliteParameter("@OrderingCustomer", message.OrderingCustomer),
            new SqliteParameter("@BeneficiaryBank", message.BeneficiaryBank),
            new SqliteParameter("@Beneficiary", message.Beneficiary),
            new SqliteParameter("@PaymentReference", message.PaymentReference),
            new SqliteParameter("@DetailsOfCharges", message.DetailsOfCharges),
            new SqliteParameter("@SenderBic", message.SenderBic),
            new SqliteParameter("@ReceiverBic", message.ReceiverBic));
    }

    public async Task<IEnumerable<SwiftMessage>> GetAllAsync()
    {
        string sql = "SELECT * FROM SwiftMessages";
        IEnumerable<SwiftMessage> messages = await _unitOfWork.QueryAsync(sql, reader => new SwiftMessage
        {
            Id = reader.GetGuid(reader.GetOrdinal("Id")),
            TransactionReferenceNumber = reader.GetString(reader.GetOrdinal("TransactionReferenceNumber")),
            BankOperationCode = reader.GetString(reader.GetOrdinal("BankOperationCode")),
            ValueDate = reader.GetString(reader.GetOrdinal("ValueDate")),
            CurrencyCode = reader.GetString(reader.GetOrdinal("CurrencyCode")),
            SettlementAmount = reader.GetDecimal(reader.GetOrdinal("SettlementAmount")),
            InstructedAmount = reader.GetDecimal(reader.GetOrdinal("InstructedAmount")),
            OrderingCustomer = reader.GetString(reader.GetOrdinal("OrderingCustomer")),
            BeneficiaryBank = reader.GetString(reader.GetOrdinal("BeneficiaryBank")),
            Beneficiary = reader.GetString(reader.GetOrdinal("Beneficiary")),
            PaymentReference = reader.GetString(reader.GetOrdinal("PaymentReference")),
            DetailsOfCharges = reader.GetString(reader.GetOrdinal("DetailsOfCharges")),
            SenderBic = reader.GetString(reader.GetOrdinal("SenderBic")),
            ReceiverBic = reader.GetString(reader.GetOrdinal("ReceiverBic")),
        });
        return messages;
    }

    public Task<SwiftMessage?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}