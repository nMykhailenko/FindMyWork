using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FindMyWork.Modular.Identity.Web.ViewModels.Account;

public class LoginViewModel
{
    [BindRequired]
    public string Username { get; set; } = null!;

    [BindRequired]
    public string Password { get; set; } = null!;

    public string ReturnUrl { get; set; } = null!;
}