using System.ComponentModel.DataAnnotations;

namespace ViewModels.RazorPages.Users;

public sealed class UserLoginViewModel
{
    [Required] public string Email { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [Display(Name = "Remember me?")] public bool RememberMe { get; set; }
}