using System.Runtime.CompilerServices;
using FindMyWork.Modules.Files.Core.Application.Common.Contracts.Database;
using FindMyWork.Modules.Files.Core.Application.Files;
using FindMyWork.Modules.Files.Core.Application.Files.Contracts;
using FindMyWork.Modules.Files.Core.Application.Files.Mappings;
using FindMyWork.Modules.Files.Core.Infrastructure.Persistence;
using FindMyWork.Modules.Files.Core.Infrastructure.Persistence.Repositories;
using FindMyWork.Shared.Infrastructure.Database.Postgres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("FindMyWork.Modules.Files.Api")]
namespace FindMyWork.Modules.Files.Core.Application.Common.Extensions;

internal static class ServiceCollectionExtensions
{
    private const string FileDbOptionsSectionName = "FileDbOptions";
    
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(FileMappingProfile));
        services.AddScoped<IFileRepository, FileRepository>();
        services.AddScoped<IFileService, FileService>();
        
        services.AddPostgres<FilesDbContext>(FileDbOptionsSectionName);

        // services.AddFluentValidation(fv =>
        // {
        //     fv.RegisterValidatorsFromAssemblyContaining<ApplicationDbContext>();
        //     fv.DisableDataAnnotationsValidation = true;
        // });
        
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        return services;
    }
}