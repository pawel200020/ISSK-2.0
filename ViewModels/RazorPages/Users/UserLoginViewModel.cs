using System.ComponentModel.DataAnnotations;
using Resources.PortalResources;
using ViewModels.Validation;

namespace ViewModels.RazorPages.Users;

public sealed class UserLoginViewModel
{
    [LocalizedRequired]
    [LocalizedDisplay(nameof(PortalResources.cUserName))]
    public string UserName { get; set; } = "";

    [LocalizedRequired]
    [LocalizedDisplay(nameof(PortalResources.cPassword))]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [LocalizedDisplay(nameof(PortalResources.cRememberMe))] 
    public bool RememberMe { get; set; }
}