using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Infrastructure.Entities.ExternalLoginProviders;

internal class ExternalLoginProviderDbModel
{   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = false;
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public ProviderType ProviderType { get; set; }
    public DateTime TsInsert { get; set; }
    public DateTime TsUpdate { get; set; }
}