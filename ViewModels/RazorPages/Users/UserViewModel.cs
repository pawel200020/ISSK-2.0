using Resources.PortalResources;
using ViewModels.Validation;

namespace ViewModels.RazorPages.Users;

public class UserViewModel
{
    [LocalizedDisplay(nameof(PortalResources.cEmail))]
    [LocalizedEmail]
    [LocalizedRequired]
    public string? Email { get; set; }
    [LocalizedDisplay(nameof(PortalResources.cFirstName))]
    [LocalizedStringLength(2, 50)]
    [LocalizedRequired]
    public string? FirstName  { get; set; }
    [LocalizedDisplay(nameof(PortalResources.cLastName))]
    [LocalizedStringLength(2, 50)]
    [LocalizedRequired]
    public string? LastName { get; set; }
    [LocalizedDisplay(nameof(PortalResources.cBirthDate))]
    [LocalizedRequired]
    public DateOnly? BirthDate { get; set; }
    [LocalizedRequired]
    public string? PhoneNumber { get; set; }
}
