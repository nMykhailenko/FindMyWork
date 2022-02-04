using System.Runtime.CompilerServices;
using FindMyWork.Modules.Files.Core.Application.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("FindMyWork.Modular.API")]
namespace FindMyWork.Modules.Files.Api;

internal static class FilesModule
{
    public const string ModulePath = "files";
    
    public static IServiceCollection AddFilesModule(this IServiceCollection services)
    {
        services.AddCore();
            
        return services;
    }

    public static IApplicationBuilder UseFilesModule(this IApplicationBuilder app)
    {
        return app;
    }
}