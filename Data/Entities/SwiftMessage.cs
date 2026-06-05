namespace SwiftParser.Data.Entities;

public sealed class SwiftMessage
{
    public int Id { get; set; }
    
    public string TransactionReferenceNumber { get; set; } = string.Empty;

    public string BankOperationCode { get; set; } = string.Empty;

    public string ValueDate { get; set; } = string.Empty;

    public string CurrencyCode { get; set; } = string.Empty;

    public string OrderingCustomer { get; set; } = string.Empty;

    public string OrderingInstitution { get; set; } = string.Empty;

    public string SenderCorrespondent { get; set; } = string.Empty;

    public string ReceiverCorrespondent { get; set; } = string.Empty;

    public string Bank { get; set; } = string.Empty;

    public string BeneficiaryBank { get; set; } = string.Empty;

    public string Beneficiary {get; set; } = string.Empty;

    public string PaymentReference { get; set; } = string.Empty;

    public string DetailsOfCharges { get; set; } = string.Empty;

    public string SenderToReceiverInformation { get; set; } = string.Empty;

    public string RegulatoryReporting { get; set; } = string.Empty;
}