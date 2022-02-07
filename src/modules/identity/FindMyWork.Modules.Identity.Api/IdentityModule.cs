using System.Runtime.CompilerServices;
using FindMyWork.Modules.Users.Core.Application.Common.Contracts.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("FindMyWork.Modular.API")]
namespace FindMyWork.Modules.Users.Api;

internal static class IdentityModule
{
    public const string ModulePath = "identity";
    
    public static IServiceCollection AddIdentityModule(this IServiceCollection services)
    {
        services.AddCore();
            
        return services;
    }

    public static IApplicationBuilder UseIdentityModule(this IApplicationBuilder app)
    {
        app.UseOpenIdDict();
        
        return app;
    }
}