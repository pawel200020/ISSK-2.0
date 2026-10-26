namespace Users.Shared.Models;

public record ApplicationUserMetadata(
    Guid Id,
    string? UserName,
    string Email,
    string FirstName,
    string LastName,
    bool IsEmailConfirmed,
    string? PhoneNumber,
    string NameAndLastName);