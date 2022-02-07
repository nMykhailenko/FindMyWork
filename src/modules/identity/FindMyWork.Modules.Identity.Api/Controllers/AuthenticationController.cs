using System.Security.Claims;
using FindMyWork.Modules.Users.Core.Domain.Entities;
using FindMyWork.Shared.Application.Models.ResponseModels;
using FindMyWork.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace FindMyWork.Modules.Users.Api.Controllers;

[ApiController]
public class AuthenticationController : BaseController
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthenticationController(
        UserManager<User> userManager, 
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("~/connect/token")]
    [Produces("application/json")]
    [Consumes("application/x-www-form-urlencoded")]
    public async Task<IActionResult> Exchange()
    {
        var oidcRequest = HttpContext.GetOpenIddictServerRequest();
        if (oidcRequest is null)
        {
            return BadRequest(new ErrorResponse(
                "AuthorizationFailed", 
                "Invalid authorization token request"));
        }
        
        if (oidcRequest.IsPasswordGrantType())
            return await TokensForPasswordGrantType(oidcRequest);

        if (oidcRequest.IsRefreshTokenGrantType())
        {
            // return tokens for refresh token flow
        }
        
        return BadRequest(new ErrorResponse(
            OpenIddictConstants.Errors.UnsupportedGrantType, 
            "Invalid grant_type."));
    }
    
    private async Task<IActionResult> TokensForPasswordGrantType(OpenIddictRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null)
            return Unauthorized();

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Request, false);
        if (!signInResult.Succeeded)
        {
            return Unauthorized();
        }
        
        var identity = new ClaimsIdentity(
            TokenValidationParameters.DefaultAuthenticationType,
            OpenIddictConstants.Claims.Name,
            OpenIddictConstants.Claims.Role);

        identity.AddClaim(
            OpenIddictConstants.Claims.Subject, 
            user.Id.ToString(), OpenIddictConstants.Destinations.AccessToken);
        identity.AddClaim(
            OpenIddictConstants.Claims.Username, 
            user.Username, OpenIddictConstants.Destinations.AccessToken);

        foreach (var userRole in user.UserRoles)
        {
            identity.AddClaim(
                OpenIddictConstants.Claims.Role, 
                userRole.Role.NormalizedName, 
                OpenIddictConstants.Destinations.AccessToken,
                OpenIddictConstants.Destinations.IdentityToken);
        }

        var claimsPrincipal = new ClaimsPrincipal(identity);
        claimsPrincipal.SetScopes(
            OpenIddictConstants.Scopes.Roles, 
            OpenIddictConstants.Scopes.OfflineAccess, 
            OpenIddictConstants.Scopes.Email, 
            OpenIddictConstants.Scopes.Profile);

        return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }
}