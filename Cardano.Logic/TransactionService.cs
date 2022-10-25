using Cardano.Domain.Dto;
using Cardano.Domain.Entity;
using Cardano.Logic.Extensions;
using Cardano.Repositories;

namespace Cardano.Logic;

public class TransactionService : ITransactionService
{
    private readonly IGleifApiRepository _gleifApiRepository;

    public TransactionService(IGleifApiRepository gleifApiRepository)
    {
        _gleifApiRepository = gleifApiRepository;
    }

    public async Task<List<Transaction>> GetTransactionsWithCostsAsync(List<TransactionDto> transactionDtos)
    {
        if (transactionDtos == null || transactionDtos.Count == 0)
            throw new Exception("No transaction found");

        if (transactionDtos.Any(transaction => string.IsNullOrEmpty(transaction.Lei)))
            throw new Exception("Some transactions does not have the Lei column");

        if (!transactionDtos.All(transaction => transaction.Rate.HasValue && transaction.Rate.Value > 0))
            throw new Exception("Some transactions does not have a valid rate, rate is required and must be greater than zero.");

        if (!transactionDtos.All(transaction => transaction.National.HasValue && transaction.National.Value > 0))
            throw new Exception("Some transactions does not have a valid National, National is required and must be greater than zero.");

        List<Transaction> transactions = new();
        // we can not send all the list of Lei to the API, so we use batch
        int batch = 10;
        int counter = 0;
        while(transactionDtos.Count > counter)
        {
            List<TransactionDto> listOfTransactions = transactionDtos.Skip(counter).Take(batch).ToList();
            string query = string.Join(",", listOfTransactions.Select(transaction => transaction.Lei).Distinct());
            Root result = await _gleifApiRepository.GetDataByLeiAsync(query);
            listOfTransactions.ForEach(transactionDto =>
            {
                Datum item = result?.Data?.FirstOrDefault(item => item?.Attributes?.Lei == transactionDto.Lei);
                if (item == null)
                    throw new Exception($"The Api did not send data for this Lei : {transactionDto.Lei}");

                Transaction transaction = transactionDto.ToTransaction();
                transaction.LegalName = item.Attributes?.Entity?.LegalName?.Name;
                if(item?.Attributes?.Bic != null && item.Attributes.Bic.Count > 0)
                    transaction.Bic = string.Join("/",item.Attributes.Bic);

                CalculateTransactionCost(transaction, item?.Attributes?.Entity?.LegalAddress?.Country);
                transactions.Add(transaction);
            });
            counter += batch;
        }

        return transactions;
    }

    private void CalculateTransactionCost(Transaction transaction, string country)
    {
        if (string.IsNullOrEmpty(country))
            return;

        transaction.Cost = country.ToUpper() switch
        {
            CountriesCode.Gb => (transaction.National * transaction.Rate) - transaction.National,
            CountriesCode.Nl => Math.Abs(decimal.Divide(transaction.National, transaction.Rate) - transaction.National),
            _ => throw new Exception("Invalid Country")
        };
    }
}
