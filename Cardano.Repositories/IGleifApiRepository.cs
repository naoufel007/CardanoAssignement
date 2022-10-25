using Cardano.Domain.Dto;

namespace Cardano.Repositories;

public interface IGleifApiRepository
{
    public Task<Root> GetDataByLeiAsync(string query);
}
