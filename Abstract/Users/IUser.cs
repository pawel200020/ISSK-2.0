namespace Abstract.Users;

public interface IUser
{
    Guid Id { get; set; }
    string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    string UserName { get; set; }
    string Email { get; set; }
    string PhoneNumber { get; set; }
    bool IsEmailConfirmed { get; set; }
    bool IsAccountDisabled { get; set; }
    string Password { get; set; }
}