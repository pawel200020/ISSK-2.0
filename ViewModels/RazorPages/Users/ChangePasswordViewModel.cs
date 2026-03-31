using System.ComponentModel.DataAnnotations;

namespace ViewModels.RazorPages.Users;

public class ChangePasswordViewModel
{
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "cOldPasswordLabel", ResourceType = typeof(Resources.PortalResources.PortalResources))]
    public string OldPassword { get; set; } = "";

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "cNewPasswordLabel", ResourceType = typeof(Resources.PortalResources.PortalResources))]
    public string NewPassword { get; set; } = "";

    [DataType(DataType.Password)]
    [Display(Name = "cConfirmPasswordLabel", ResourceType = typeof(Resources.PortalResources.PortalResources))]
    [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources.PortalResources.PortalResources), ErrorMessageResourceName = "cPasswordConfirmMismatch")]
    public string ConfirmPassword { get; set; } = "";
}