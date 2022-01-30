using System.Runtime.CompilerServices;
using FindMyWork.Modules.Jobs.Core.Application.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("FindMyWork.Modular.API")]
namespace FIndMyWork.Modules.Jobs.Api;

internal static class JobsModule
{
    public const string ModulePath = "jobs";
    
    public static IServiceCollection AddJobsModule(this IServiceCollection services)
    {
        services.AddCore();
            
        return services;
    }

    public static IApplicationBuilder UseJobsModule(this IApplicationBuilder app)
    {
        return app;
    }
}