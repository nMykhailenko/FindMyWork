using System.Security.Claims;
using FindMyWork.Modular.Identity.Web.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindMyWork.Modular.Identity.Web.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]/[action]")]
public class AccountController : Controller
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View("Login");
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([Bind("Username,Password,ReturnUrl")]LoginViewModel model)
    {
        ViewData["ReturnUrl"] = model.ReturnUrl;

        if (!ModelState.IsValid) return View(model);
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, model.Username)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

        if (Url.IsLocalUrl(model.ReturnUrl))
        {
            return Redirect(model.ReturnUrl);
        }

        return View(model);

    }
}