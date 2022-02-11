using System.Security.Claims;
using FindMyWork.Modules.Identity.Core.Domain.Entities;
using FindMyWork.Shared.Application.Models.ResponseModels;
using FindMyWork.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace FindMyWork.Modular.Identity.Web.Controllers;

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
    
    [HttpGet("~/connect/authorize")]
    [HttpPost("~/connect/authorize")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Authorize()
    {
        var request = HttpContext.GetOpenIddictServerRequest() ??
                      throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!result.Succeeded)
        {
            return Challenge(
                authenticationSchemes: CookieAuthenticationDefaults.AuthenticationScheme,
                properties: new AuthenticationProperties
                {
                    RedirectUri = Request.PathBase + Request.Path + QueryString.Create(
                        Request.HasFormContentType ? Request.Form.ToList() : Request.Query.ToList())
                });
        }

        // Create a new claims principal
        var claims = new List<Claim>
        {
            // 'subject' claim which is required
            new Claim(OpenIddictConstants.Claims.Subject, result.Principal.Identity.Name),
            new Claim("some claim", "some value").SetDestinations(OpenIddictConstants.Destinations.AccessToken)
        };

        var claimsIdentity = new ClaimsIdentity(claims, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        // Set requested scopes (this is not done automatically)
        claimsPrincipal.SetScopes(request.GetScopes());

        return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
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