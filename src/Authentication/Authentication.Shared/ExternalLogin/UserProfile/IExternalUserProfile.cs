namespace Authentication.Shared.ExternalLogin.UserProfile;

public interface IExternalUserProfile
{
    string? FirstName { get; }
    string? LastName { get;  }
    string? PhoneNumber { get; }
    string? Email { get; }
    DateOnly? BirthDate { get; }
}