using Microsoft.Extensions.DependencyInjection;

namespace Cardano.Logic;

public static class ServiceCollection
{
    public static void ServiceCollectionForServices(this IServiceCollection services)
    {
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<ICsvFileService, CsvFileService>();
    }
}