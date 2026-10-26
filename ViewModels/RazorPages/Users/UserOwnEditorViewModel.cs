using Resources.PortalResources;
using ViewModels.Validation;

namespace ViewModels.RazorPages.Users;

public class UserOwnEditorViewModel
{
    [LocalizedRequired]
    [LocalizedDisplay(nameof(PortalResources.cPhone))]
    public string? PhoneNumber { get; set; }
    [LocalizedRequired]
    [LocalizedDisplay(nameof(PortalResources.cBirthDate))]
    public DateOnly? BirthDate { get; set; }    
}