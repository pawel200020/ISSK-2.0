#nullable disable
using Abstract.Users;

namespace Core.BusinessEntities;

public class User : IUser
{
    public Guid Id { get; set; }
    public string NickName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RoleId { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public bool IsAccountDisabled { get; set; }
}