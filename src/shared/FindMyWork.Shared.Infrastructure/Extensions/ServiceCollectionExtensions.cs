using System.Runtime.CompilerServices;
using Azure.Storage.Blobs;
using FindMyWork.Shared.Application.Contracts;
using FindMyWork.Shared.Application.Options;
using FindMyWork.Shared.Infrastructure.Controllers;
using FindMyWork.Shared.Infrastructure.Services.Storage;
using FindMyWork.Shared.Infrastructure.Services.Storage.Factory;
using FindMyWork.Shared.Infrastructure.Validators;
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
        
        services.AddTransient<IValidationFactory, ValidatorFactory>();

        var blobStorageOption = services.GetOptions<StorageAccountOptions>(nameof(StorageAccountOptions));
        services.AddScoped(_ => new BlobServiceClient(blobStorageOption.ConnectionString));
        services.AddScoped<IBlobContainerNameFactory, BlobContainerNameFactory>();
        services.AddScoped<IBlobStorageService, BlobStorageService>();

        services.AddSwaggerGen();
        
        return services;
    }
}