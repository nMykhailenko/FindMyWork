using System.Runtime.CompilerServices;
using FindMyWork.Modules.Identity.Core.Application.Users;
using FindMyWork.Modules.Identity.Core.Domain.Entities;
using FindMyWork.Modules.Identity.Core.Infrastructure.Persistence;
using FindMyWork.Shared.Infrastructure.Database.Postgres;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;

[assembly:InternalsVisibleTo("FindMyWork.Modular.Identity.Web")]
namespace FindMyWork.Modules.Identity.Core.Application.Common.Contracts.Extensions;

internal static class ServiceCollectionExtensions
{
    private const string IdentityDbOptionsSectionName = "IdentityDbOptions";
    
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddControllersWithViews();
        
        services.AddIdentityPostgres<IdentityDbContext>(IdentityDbOptionsSectionName);
        services.Configure<IdentityOptions>(options =>
        {
            options.ClaimsIdentity.UserNameClaimType = OpenIddictConstants.Claims.Name;
            options.ClaimsIdentity.UserIdClaimType = OpenIddictConstants.Claims.Subject;
            options.ClaimsIdentity.RoleClaimType = OpenIddictConstants.Claims.Role;
        });
        
        services.AddOpenIddict()
            .AddCore(options => options.UseEntityFrameworkCore().UseDbContext<IdentityDbContext>())
            .AddServer(options =>
            {
                // Enable the required endpoints
                options.SetAuthorizationEndpointUris("/connect/authorize");
                options.SetTokenEndpointUris("/connect/token");
                options.SetUserinfoEndpointUris("/connect/userinfo");

                options.AllowPasswordFlow();
                options.AllowRefreshTokenFlow();
                options.AllowAuthorizationCodeFlow().RequireProofKeyForCodeExchange();
                options.AllowClientCredentialsFlow();
                
                options.UseReferenceAccessTokens();
                options.UseReferenceRefreshTokens();
                
                options.RegisterScopes(OpenIddictConstants.Permissions.Scopes.Email,
                                OpenIddictConstants.Permissions.Scopes.Profile,
                                OpenIddictConstants.Permissions.Scopes.Roles);

                options.SetAccessTokenLifetime(TimeSpan.FromMinutes(60));
                options.SetRefreshTokenLifetime(TimeSpan.FromDays(7));

                options.AddDevelopmentEncryptionCertificate()
                    .AddDevelopmentSigningCertificate();

                options.UseAspNetCore()
                    .EnableTokenEndpointPassthrough()
                    .EnableAuthorizationEndpointPassthrough();
            })
            .AddValidation(options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();
            });

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = OpenIddictConstants.Schemes.Bearer;
            options.DefaultChallengeScheme = OpenIddictConstants.Schemes.Bearer;
        });
        
        services.AddIdentity<User, Role>()
            .AddSignInManager()
            .AddUserStore<UserStore>()
            .AddRoleStore<RoleStore>()
            .AddUserManager<UserManager<User>>();
        
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/account/login";
            });
        
        return services;
    }
}