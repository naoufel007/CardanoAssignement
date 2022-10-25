using Cardano.Domain.Enum;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Cardano.Logic.Extensions;

public class TransactionTypeMapperExtension : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrEmpty(text) || text.ToLower() != TransactionType.Sell.ToString().ToLower())
            return TransactionType.Buy;

        return TransactionType.Sell;
    }
}
