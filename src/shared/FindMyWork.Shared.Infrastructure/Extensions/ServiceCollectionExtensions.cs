using System.Runtime.CompilerServices;
using FindMyWork.Shared.Infrastructure.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


[assembly:InternalsVisibleTo("FindMyWork.Modular.API")]
namespace FindMyWork.Shared.Infrastructure.Extensions;

internal static class ServiceCollectionExtensions
{
    public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var section = configuration.GetSection(sectionName);
        var options = new T();
        
        section.Bind(options);
            
        return options;
    }

    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
    {
        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });    
        
        return services;
    }
}