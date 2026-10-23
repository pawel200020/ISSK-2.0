using System.ComponentModel.DataAnnotations;
using Resources.PortalResources;
using ViewModels.Validation;

namespace ViewModels.RazorPages.Users;

public class UserAdminCreateViewModel : UserViewModel
{
    public string? Id { get; set; }
    
    [LocalizedRequired]
    [LocalizedDisplay(nameof(PortalResources.cUserName))]
    public string UserName { get; set; } = null!;
    
    public bool IsDisabled { get; set; }

    [LocalizedRequired] 
    [DataType(DataType.Password)]
    [LocalizedDisplay(nameof(PortalResources.cPassword))]
    [LocalizedStringLength(6, 100)]
    public string Password { get; set; } = null!;

    [LocalizedRequired]
    [DataType(DataType.Password)]
    [LocalizedDisplay(nameof(PortalResources.cConfirmPassword))]
    [Compare("Password", ErrorMessageResourceType = typeof(PortalResources), ErrorMessageResourceName = "cPasswordMismatchError")]
    public string ConfirmPassword { get; set; } = null!;
    
    [LocalizedRequired]
    public Guid RoleId { get; set; }
}