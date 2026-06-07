using SwiftParser.Domain.Entities;
using SwiftParser.Repositories.Interfaces;
using SwiftParser.Application.Interfaces;
using SwiftParser.Application.Mappers;
using SwiftParser.Application.DTOs;
using static SwiftParser.Shared.Utilities;

namespace SwiftParser.Application.Implementations;

public sealed class SwiftParserService(IUnitOfWork unitOfWork, ISwiftRepository swiftRepository, ILogRepository logRepository) : ISwiftParserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISwiftRepository _swiftRepository = swiftRepository;
    private readonly ILogRepository _logRepository = logRepository;

    public async Task<List<SwiftMessageDTO>> QueryAllAsync()
    {
        IEnumerable<SwiftMessage> messages = await _swiftRepository.GetAllAsync().ConfigureAwait(false);
        return [.. messages.Select(message => message.ToDTO())];
    }

    public async Task<SwiftMessageDTO> ParseMessageAsync(IFormFile file)
    {
        string content;
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            content = await reader.ReadToEndAsync();
        }

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("The uploaded file is empty!");
        }

        SwiftMessage swiftMessage = new()
        {
            TransactionReferenceNumber = content.GetTag("20"),
            BankOperationCode = content.GetTag("23B"),
            ValueDate = content.GetTag("32A")[..6],
            CurrencyCode = content.GetTag("32A")[6..9],
            SettlementAmount = decimal.Parse(
                        content.GetTag("32A")[9..].Replace(',', '.'),
                        System.Globalization.CultureInfo.InvariantCulture),
            InstructedAmount = decimal.Parse(
                        content.GetTag("33B")[3..].Replace(',', '.'),
                        System.Globalization.CultureInfo.InvariantCulture),
            OrderingCustomer = content.GetTag("50K").Replace("\r\n", " ").Trim(),
            BeneficiaryBank = content.GetTag("57A"),
            Beneficiary = content.GetTag("59").Replace("\r\n", " ").Trim(),
            PaymentReference = content.GetTag("70").Replace("\r\n", " ").Trim(),
            DetailsOfCharges = content.GetTag("71A"),
            SenderBic = content.GetTag("52A"),
            ReceiverBic = content.GetTag("53A")
        };

        _unitOfWork.BeginTransaction();

        try
        {
            await _swiftRepository.AddAsync(swiftMessage);
            await _logRepository.AddAsync(new()
            {
                Message = $"Uploaded message {swiftMessage.Id} into the database!",
                Timestamp = DateTime.Now
            });
            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }

        return swiftMessage.ToDTO();
    }

    public async Task<SwiftMessageDTO?> GetByIdAsync(Guid id)
    {
        SwiftMessage? swiftMessage = await _swiftRepository.GetByIdAsync(id);

        if (swiftMessage is null)
        {
            return null;
        }

        return swiftMessage.ToDTO();
    }

    public async Task<bool> DeleteMessageAsync(Guid id)
    {
        SwiftMessage? swiftMessage = await _swiftRepository.GetByIdAsync(id);

        if (swiftMessage is null)
        {
            await _logRepository.AddAsync(new()
            {
                Message = $"Attempted to delete log with wrong id! {id}",
                Timestamp = DateTime.Now
            });
            return false;
        }

        _unitOfWork.BeginTransaction();

        try
        {
            await _swiftRepository.DeleteAsync(id);
            await _logRepository.AddAsync(new()
            {
                Message = $"Deleted swift message with id {id} successfully!",
                Timestamp = DateTime.Now
            });

            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
            return false;
        }

        return true;
    }
}