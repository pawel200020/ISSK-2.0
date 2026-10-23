using System.ComponentModel.DataAnnotations;
using Resources.PortalResources;
using ViewModels.Validation;

namespace ViewModels.RazorPages.Users
{
    public class ResetPasswordViewModel
    {
        [LocalizedRequired]
        [LocalizedStringLength(6, 100, ErrorMessageResourceType = typeof(PortalResources), ErrorMessageResourceName = "cPasswordLengthError")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [LocalizedDisplay(nameof(PortalResources.cConfirmPassword))]
        [Compare("Password", ErrorMessageResourceType = typeof(PortalResources), ErrorMessageResourceName = "cPasswordMismatchError")]
        public string ConfirmPassword { get; set; } = "";
    }
}
