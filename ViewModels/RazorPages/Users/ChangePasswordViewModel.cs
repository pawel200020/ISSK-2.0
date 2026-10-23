using System.ComponentModel.DataAnnotations;
using ViewModels.Validation;

namespace ViewModels.RazorPages.Users;

public class ChangePasswordViewModel
{
    [LocalizedRequired]
    [DataType(DataType.Password)]
    [LocalizedDisplay(nameof(Resources.PortalResources.PortalResources.cOldPasswordLabel))]
    public string OldPassword { get; set; } = "";

    [LocalizedRequired]
    [LocalizedDisplay(nameof(Resources.PortalResources.PortalResources.cNewPasswordLabel))]
    [LocalizedStringLength(6, 100)]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; } = "";

    [DataType(DataType.Password)]
    [LocalizedDisplay(nameof(Resources.PortalResources.PortalResources.cConfirmPasswordLabel))]
    [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources.PortalResources.PortalResources), ErrorMessageResourceName = "cPasswordConfirmMismatch")]
    public string ConfirmPassword { get; set; } = "";
}