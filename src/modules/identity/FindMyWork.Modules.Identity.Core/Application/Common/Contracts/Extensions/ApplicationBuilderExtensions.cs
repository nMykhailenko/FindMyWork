using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;

[assembly:InternalsVisibleTo("FindMyWork.Modules.Identity.Api")]
namespace FindMyWork.Modules.Users.Core.Application.Common.Contracts.Extensions;

internal static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseOpenIdDict(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}