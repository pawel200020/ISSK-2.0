using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
#nullable disable

namespace Users.Models;

public class ApplicationUser : IdentityUser
{
    [Column(TypeName = "VARCHAR")]
    [StringLength(50)]
    [NotNull]
    public required string FirstName { get; set; }
    [Column(TypeName = "VARCHAR")]
    [StringLength(50)]
    [NotNull]
    public required string LastName { get; set; }
    public required DateOnly BirthDate { get; set; }
    public required DateTime TsInsert { get; set; }
}