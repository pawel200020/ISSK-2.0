using System.ComponentModel.DataAnnotations;

namespace ViewModels.RazorPages.Users;

public class UserAdminViewModel
{
    public string Id { get; set; }
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
    
    public bool IsAccountDisabled { get; set; }
    public bool IsEmailConfirmed { get; set; }
    
    [Required]
    [Phone]
    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; } = null!;
    
    [Required]
    [Display(Name = "Role")]
    public Guid RoleId { get; set; }
}