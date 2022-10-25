using Microsoft.Extensions.DependencyInjection;

namespace Cardano.Repositories;

public static class ServiceCollection
{
    public static void ServiceCollectionForRepositories(this IServiceCollection services)
    {
        services.AddScoped<IGleifApiRepository, GleifApiRepository>();
    }
}