using System.ComponentModel.DataAnnotations;

namespace ViewModels.RazorPages.Users;

public class UserCreateViewModel
{
    public Guid Id { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
    [Display(Name = "First name")]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
    [Display(Name = "Last name")]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = null!;
    
    [Required]
    [Display(Name = "Nick")]
    public string UserName { get; set; } = null!;
    
    [Required]
    [Display(Name = "Birth date")]
    [DataType(DataType.Date)]
    public DateOnly? BirthDate { get; set; }    
    
    public bool IsDisabled { get; set; }
    
    [Required]
    [Phone]
    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; } = null!;
    public string testParam { get; set; } = null!;

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = null!;
}