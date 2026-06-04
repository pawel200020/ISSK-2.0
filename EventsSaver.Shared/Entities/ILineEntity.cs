using EventsSaver.Shared.Entities.Enums;

namespace EventsSaver.Shared.Entities;

public interface ILineEntity
{
    Guid Id { get; set; }
    string Number { get; set; }
    DateOnly Date { get; set; }
    string Vehicle { get; set; }
    Guid SupervisorId { get; }
    ISeasonEntity Season { get; set; }
    LineType LineType { get; set; }
    bool IsActive { get; set; }
}