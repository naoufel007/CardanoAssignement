using Cardano.Domain.Dto;
using Cardano.Domain.Entity;
using Cardano.Logic.Mappers;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Cardano.Logic;

public class CsvFileService : ICsvFileService
{
    public static Dictionary<Type, ClassMap> Mapping = new Dictionary<Type, ClassMap>();
    public async Task<IEnumerable<T>> LoadDataFromCsvFileAsync<T>(Stream stream)
    {
        List<T> records = new();
        using (StreamReader reader = new(stream))
        using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            if (Mapping.ContainsKey(typeof(T)))
            {
                csv.Context.RegisterClassMap(Mapping[typeof(T)]);
            }           
            await foreach(T record in csv.GetRecordsAsync<T>())
            {
                records.Add(record);
            }
        }
        return records;
    }

    public async Task<Byte[]> CreateCsvFileFromDataAsync<T>(IEnumerable<T> records)
    {
        using MemoryStream stream = new();
        using StreamWriter writer = new(stream);
        using CsvWriter csv = new(writer, CultureInfo.InvariantCulture);
        if (Mapping.ContainsKey(typeof(T)))
        {
            csv.Context.RegisterClassMap(Mapping[typeof(T)]);
        }
        await csv.WriteRecordsAsync(records);
        await csv.FlushAsync();
        stream.Position = 0;
        return stream.ToArray();
    }
}
