using FindMyWork.Modules.Users.Core.Application.Common.Contracts.Database;
using FindMyWork.Modules.Users.Core.Application.Users;
using FindMyWork.Modules.Users.Core.Domain.Entities;
using FindMyWork.Modules.Users.Core.Infrastructure.Persistence;
using FindMyWork.Shared.Infrastructure.Database.Postgres;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;

namespace FindMyWork.Modules.Users.Core.Application.Common.Contracts.Extensions;

internal static class ServiceCollectionExtensions
{
    private const string IdentityDbOptionsSectionName = "IdentityDbOptions";
    
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
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
            options.SetTokenEndpointUris("/connect/token");
            options.SetUserinfoEndpointUris("/connect/userinfo");

            options.AllowPasswordFlow();
            options.AllowRefreshTokenFlow();
            options.AllowAuthorizationCodeFlow();
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

            options.UseAspNetCore().EnableTokenEndpointPassthrough();
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
        
        return services;
    }
}