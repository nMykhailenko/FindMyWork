using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;

[assembly:InternalsVisibleTo("FindMyWork.Modules.Identity.Web")]
namespace FindMyWork.Modules.Identity.Core.Application.Common.Contracts.Extensions;

internal static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseOpenIdDict(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        InitializeOAuthClient(app);
        
        return app;
    }

    private static void InitializeOAuthClient(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
        var existingClientApp = manager.FindByClientIdAsync("postman").GetAwaiter().GetResult();
        if (existingClientApp == null)
        {
            manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "postman",
                ClientSecret = "499D56FA-B47B-5199-BA61-B298D431C318",
                DisplayName = "Postman client application",
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.Token,

                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,

                    OpenIddictConstants.Permissions.Prefixes.Scope + "api",

                    OpenIddictConstants.Permissions.ResponseTypes.Code
                },
                RedirectUris = { new Uri("https://www.getpostman.com/oauth2/callback") },
            }).GetAwaiter().GetResult();
        }
    }
}