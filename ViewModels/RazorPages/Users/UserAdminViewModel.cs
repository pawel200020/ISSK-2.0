using Resources.PortalResources;
using ViewModels.Validation;

namespace ViewModels.RazorPages.Users;

public class UserAdminViewModel : UserViewModel
{
    public string? Id { get; set; }
    
    [LocalizedRequired]
    [LocalizedDisplay(nameof(PortalResources.cUserName))]
    [LocalizedStringLength(2, 50)]
    public string UserName { get; set; } = null!;
    
    public bool IsAccountDisabled { get; set; }
    public bool IsEmailConfirmed { get; set; }
    
    [LocalizedRequired]
    public Guid RoleId { get; set; }
}