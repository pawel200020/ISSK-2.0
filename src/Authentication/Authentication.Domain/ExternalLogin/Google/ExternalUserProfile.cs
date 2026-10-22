using Authentication.Shared.ExternalLogin.UserProfile;

namespace Authentication.Domain.ExternalLogin.Google;

public class ExternalUserProfile : IExternalUserProfile
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public DateOnly? BirthDate { get; set; }
}