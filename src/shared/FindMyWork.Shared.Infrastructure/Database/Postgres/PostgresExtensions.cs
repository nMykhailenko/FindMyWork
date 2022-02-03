using FindMyWork.Shared.Infrastructure.Database.Repositories;
using FindMyWork.Shared.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FindMyWork.Shared.Infrastructure.Database.Postgres;

public static class PostgresExtensions
{
    internal static IServiceCollection AddPostgres(this IServiceCollection services)
    {
        var options = services.GetOptions<PostgresOptions>("postgres");
        services.AddSingleton(options);

        return services;
    }

    public static IServiceCollection AddPostgres<T>(this IServiceCollection services) where T : BaseDbContext
    {
        var options = services.GetOptions<PostgresOptions>("postgres");
        services.AddDbContext<T>(x => x.UseNpgsql(options.ConnectionString));

        // using var scope = services.BuildServiceProvider().CreateScope();
        // var dbContext = scope.ServiceProvider.GetRequiredService<T>();
        // dbContext.Database.Migrate();

        return services;
    }
}