using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.DatabaseEnums;

namespace Data.Entites.Configuration;

public class DictParameterType
{
    protected DictParameterType() { }
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    [MaxLength(40)]
    public required string Name { get; set; }
    //[NotMapped]
    //public ParameterTypeEnum Type => (ParameterTypeEnum)Enum.Parse(typeof(ParameterTypeEnum), Name);
}