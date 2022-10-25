using Cardano.Domain.Dto;
using Cardano.Domain.Entity;

namespace Cardano.Logic;

public interface ICsvFileService
{
    public Task<IEnumerable<T>> LoadDataFromCsvFileAsync<T>(Stream stream);
    public Task<Byte[]> CreateCsvFileFromDataAsync<T>(IEnumerable<T> records);
}
