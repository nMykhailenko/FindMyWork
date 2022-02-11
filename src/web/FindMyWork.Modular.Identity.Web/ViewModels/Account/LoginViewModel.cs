using System.ComponentModel.DataAnnotations;

namespace FindMyWork.Modular.Identity.Web.ViewModels.Account;

public record LoginViewModel
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    public string ReturnUrl { get; set; } = null!;
}