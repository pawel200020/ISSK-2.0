namespace Users.Shared.Models;

public interface IUser
{
    Guid Id { get; }
    string FirstName { get; }
    public string LastName { get; }
    public DateOnly BirthDate { get; }
    string UserName { get; }
    string Email { get; }
    string PhoneNumber { get; }
    bool IsAccountDisabled { get; }
    bool IsEmailConfirmed { get; }
    string Password { get; }
    Guid RoleId { get; }
}