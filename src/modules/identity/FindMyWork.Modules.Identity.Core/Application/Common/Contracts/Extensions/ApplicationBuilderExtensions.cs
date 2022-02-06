using Microsoft.AspNetCore.Builder;

namespace FindMyWork.Modules.Users.Core.Application.Common.Contracts.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseOpenIdDict(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}