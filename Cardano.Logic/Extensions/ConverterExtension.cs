using Cardano.Domain.Dto;
using Cardano.Domain.Entity;

namespace Cardano.Logic.Extensions;

public static class ConverterExtension
{
    public static Transaction ToTransaction(this TransactionDto transactionDto)
    {
        if (transactionDto == null)
            return null;

        return new Transaction
        {
            Uti = transactionDto.Uti,
            Isin = transactionDto.Isin,
            National = transactionDto.National.Value,
            NationalCurrency = transactionDto.NationalCurrency,
            Type = transactionDto.Type.ToString(),
            DateTime = transactionDto.DateTime,
            Rate = transactionDto.Rate.Value,
            Lei = transactionDto.Lei,
        };
    }
}
