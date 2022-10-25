using Cardano.Domain.Dto;
using System.Text.Json;

namespace Cardano.Repositories;

public class GleifApiRepository : IGleifApiRepository
{
    private readonly HttpClient _httpClient;
    private static readonly JsonSerializerOptions options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public GleifApiRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Root> GetDataByLeiAsync(string query)
    {
        var response = await _httpClient.GetAsync($"v1/lei-records?filter[lei]={query}");
        string content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Root>(content, options);
    }
}
