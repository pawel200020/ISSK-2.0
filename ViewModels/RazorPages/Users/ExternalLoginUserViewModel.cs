namespace ViewModels.RazorPages.Users;

public class ExternalLoginUserViewModel
{
    public string Nickname { get; set; }
    public string Email { get; set; }
    public string FirstName  { get; set; }
    public string LastName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string PhoneNumber { get; set; }
}
