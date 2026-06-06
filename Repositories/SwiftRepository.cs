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
                    CurrencyCode, Amount, OrderingCustomer, BeneficiaryBank, Beneficiary,
                    PaymentReference, DetailsOfCharges, SenderBic, ReceiverBic)
            VALUES 
                    (@Id, @TransactionReferenceNumber, @BankOperationCode, @ValueDate, @CurrencyCode,
                    @Amount, @OrderingCustomer, @BeneficiaryBank, @Beneficiary, @PaymentReference,
                    @DetailsOfCharges, @SenderBic, @ReceiverBic);
            """;

        await _unitOfWork.ExecuteAsync(sql,
            new SqliteParameter("@Id", message.Id),
            new SqliteParameter("@TransactionReferenceNumber", message.TransactionReferenceNumber),
            new SqliteParameter("@BankOperationCode", message.BankOperationCode),
            new SqliteParameter("@ValueDate", message.ValueDate),
            new SqliteParameter("@CurrencyCode", message.CurrencyCode),
            new SqliteParameter("@Amount", message.Amount),
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
        throw new NotImplementedException();
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