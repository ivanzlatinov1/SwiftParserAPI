namespace SwiftParser.Data.Entities;

public sealed class SwiftMessage
{
    public SwiftMessage()
    {
        Id = Guid.CreateVersion7();
    }

    public Guid Id { get; init; }
    public string TransactionReferenceNumber { get; set; } = string.Empty;
    public string BankOperationCode { get; set; } = string.Empty;
    public string ValueDate { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public decimal SettlementAmount { get; set; }
    public decimal InstructedAmount { get; set; }
    public string OrderingCustomer { get; set; } = string.Empty;
    public string BeneficiaryBank { get; set; } = string.Empty;
    public string Beneficiary { get; set; } = string.Empty;
    public string PaymentReference { get; set; } = string.Empty;
    public string DetailsOfCharges { get; set; } = string.Empty;
    public string SenderBic { get; set; } = string.Empty;
    public string ReceiverBic { get; set; } = string.Empty;
}