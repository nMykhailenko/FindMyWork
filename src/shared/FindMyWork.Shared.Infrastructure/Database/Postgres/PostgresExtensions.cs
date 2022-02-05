using FindMyWork.Shared.Infrastructure.Database.Repositories;
using FindMyWork.Shared.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FindMyWork.Shared.Infrastructure.Database.Postgres;

public static class PostgresExtensions
{
    internal static IServiceCollection AddPostgres(this IServiceCollection services, string sectionName)
    {
        var options = services.GetOptions<PostgresOptions>(sectionName);
        services.AddSingleton(options);

        return services;
    }

    public static IServiceCollection AddPostgres<T>(
        this IServiceCollection services,
        string sectionName) where T : BaseDbContext
    {
        var options = services.GetOptions<PostgresOptions>(sectionName);
        services.AddDbContext<T>(x => x.UseNpgsql(options.ConnectionString));

        using var scope = services.BuildServiceProvider().CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<T>();
        dbContext.Database.Migrate();

        return services;
    }
}