using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entites.EventSaver;

public class SeasonDb
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required]
    public required string Name { get; set; }
    [Required]
    public DateOnly StartDate { get; set; }
}