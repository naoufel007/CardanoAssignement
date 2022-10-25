using Cardano.Domain.Dto;
using Cardano.Domain.Entity;

namespace Cardano.Logic;

public interface ITransactionService
{
    public Task<List<Transaction>> GetTransactionsWithCostsAsync(List<TransactionDto> transactionDtos);
}
