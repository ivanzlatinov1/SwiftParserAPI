using SwiftParser.Data.Entities;
using SwiftParser.DTOs;

namespace SwiftParser.Services.Mappers;

public static class SwiftMessageMapper
{
    extension(SwiftMessage message)
    {
        public SwiftMessageDTO ToDTO()
        {
            return new SwiftMessageDTO
            {
                Id = message.Id,
                TransactionReferenceNumber = message.TransactionReferenceNumber,
                BankOperationCode = message.BankOperationCode,
                ValueDate = message.ValueDate,
                CurrencyCode = message.CurrencyCode,
                Amount = message.Amount,
                OrderingCustomer = message.OrderingCustomer,
                BeneficiaryBank = message.BeneficiaryBank,
                Beneficiary = message.Beneficiary,
                PaymentReference = message.PaymentReference,
                DetailsOfCharges = message.DetailsOfCharges,
                SenderBic = message.SenderBic,
                ReceiverBic = message.ReceiverBic
            };
        }
    }

    extension(SwiftMessageDTO messageDTO)
    {
        public SwiftMessage ToEntity()
        {
            return new SwiftMessage
            {
                Id = messageDTO.Id,
                TransactionReferenceNumber = messageDTO.TransactionReferenceNumber,
                BankOperationCode = messageDTO.BankOperationCode,
                ValueDate = messageDTO.ValueDate,
                CurrencyCode = messageDTO.CurrencyCode,
                Amount = messageDTO.Amount,
                OrderingCustomer = messageDTO.OrderingCustomer,
                BeneficiaryBank = messageDTO.BeneficiaryBank,
                Beneficiary = messageDTO.Beneficiary,
                PaymentReference = messageDTO.PaymentReference,
                DetailsOfCharges = messageDTO.DetailsOfCharges,
                SenderBic = messageDTO.SenderBic,
                ReceiverBic = messageDTO.ReceiverBic
            };
        }
    }
}