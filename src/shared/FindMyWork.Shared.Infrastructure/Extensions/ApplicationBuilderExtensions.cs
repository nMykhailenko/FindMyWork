using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;


[assembly:InternalsVisibleTo("FindMyWork.Modular.API")]
[assembly:InternalsVisibleTo("FindMyWork.Modular.Identity.Web")]
namespace FindMyWork.Shared.Infrastructure.Extensions;

internal static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSharedInfrastructure(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        return app;
    }
}