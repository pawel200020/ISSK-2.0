using System.ComponentModel.DataAnnotations;
using Resources.PortalResources;

namespace ViewModels.RazorPages.Users
{
    public class ResetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(PortalResources), ErrorMessageResourceName = "cPasswordLengthError", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceType = typeof(PortalResources), ErrorMessageResourceName = "cPasswordMismatchError")]
        public string ConfirmPassword { get; set; } = "";
    }
}
