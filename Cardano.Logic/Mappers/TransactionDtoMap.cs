using Cardano.Domain.Dto;
using Cardano.Logic.Extensions;
using CsvHelper.Configuration;

namespace Cardano.Logic.Mappers;

public class TransactionDtoMap : ClassMap<TransactionDto>
{
    public TransactionDtoMap()
    {
        Map(p => p.Uti).Name("transaction_uti");
        Map(p => p.Isin).Name("isin");
        Map(p => p.National).Name("notional").TypeConverter<DecimalMapperExtension>();
        Map(p => p.NationalCurrency).Name("notional_currency");
        Map(p => p.Type).Name("transaction_type").TypeConverter<TransactionTypeMapperExtension>();
        Map(p => p.DateTime).Name("transaction_datetime").TypeConverter<DateTimeMapperExtension>();
        Map(p => p.Rate).Name("rate");
        Map(p => p.Lei).Name("lei");
    }
}
