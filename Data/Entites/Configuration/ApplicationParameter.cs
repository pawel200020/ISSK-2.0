using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entites.Configuration;

public class ApplicationParameter
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }
    [MaxLength(50)]
    public string? Value { get; set; }
    public DateTime TsInsert { get; set; }
    public DateTime TsUpdate { get; set; }
    public required DictParameterType ParameterType { get; set; }
}