namespace Users.Shared.Models;

public record ApplicationUserMetadata(Guid Id, string Email, string FirstName, string LastName, bool IsEmailConfirmed);