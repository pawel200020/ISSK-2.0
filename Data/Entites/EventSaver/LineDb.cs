using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Entites.EventSaver.Enums;
using Users.Shared.Models;

namespace Data.Entites.EventSaver;

public class LineDb
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public required string Number { get; set; }
    public DateOnly Date { get; set; }
    public required string Vehicle { get; set; }
    public string SupervisorId { get; set; }
    public virtual ApplicationUser Supervisor { get; set; } = null!;
    public Guid SeasonId { get; set; }
    public  SeasonDb Season { get; set; }
    public required LineTypeDb LineType { get; set; }
    public bool IsActive { get; set; }
    public DateTime TsInsert { get; set; }
    public DateTime TsUpdate { get; set; }
}