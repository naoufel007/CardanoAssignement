using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace Cardano.Logic.Extensions;

public class DecimalMapperExtension : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        double value = Convert.ToDouble(text, CultureInfo.InvariantCulture);
        return (decimal)value;
    }
}
