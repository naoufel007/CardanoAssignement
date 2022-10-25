using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace Cardano.Logic.Extensions;

public class DateTimeMapperExtension : DefaultTypeConverter
{
    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        DateTime datetime = (DateTime)value;
        return datetime.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
    }

    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        return DateTime.ParseExact(text, "yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture);
    }
}
