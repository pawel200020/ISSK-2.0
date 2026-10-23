using System.ComponentModel.DataAnnotations;
using Resources.PortalResources;
using ViewModels.Validation;

namespace ViewModels.RazorPages.Users;

public class UserCreateViewModel : UserViewModel
{
    [LocalizedRequired]
    [LocalizedDisplay(nameof(PortalResources.cUserName))]
    public string UserName { get; set; } = null!;

    [LocalizedRequired]
    [LocalizedDisplay(nameof(PortalResources.cPassword))]
    [LocalizedStringLength(6,100)] 
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [LocalizedDisplay(nameof(PortalResources.cConfirmPassword))]
    [LocalizedCompare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;
}
