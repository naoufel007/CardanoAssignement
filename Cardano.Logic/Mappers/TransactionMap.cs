using Cardano.Domain.Entity;
using Cardano.Logic.Extensions;
using CsvHelper.Configuration;

namespace Cardano.Logic.Mappers;

public class TransactionMap : ClassMap<Transaction>
{
    public TransactionMap()
    {
        Map(p => p.Uti).Name("transaction_uti");
        Map(p => p.Isin).Name("isin");
        Map(p => p.National).Name("notional").TypeConverter<DecimalMapperExtension>();
        Map(p => p.NationalCurrency).Name("notional_currency");
        Map(p => p.Type).Name("transaction_type");
        Map(p => p.DateTime).Name("transaction_datetime").TypeConverter<DateTimeMapperExtension>();
        Map(p => p.Rate).Name("rate");
        Map(p => p.Lei).Name("lei");
        Map(p => p.LegalName).Name("legal_name");
        Map(p => p.Bic).Name("bic");
        Map(p => p.Cost).Name("transaction_costs");
    }
}
