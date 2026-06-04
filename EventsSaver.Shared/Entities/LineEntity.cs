using EventsSaver.Shared.Entities.Enums;

namespace EventsSaver.Shared.Entities;

public class LineEntity : ILineEntity
{
    public Guid Id { get; set; }
    public string Number { get; set; }
    public DateOnly Date { get; set; }
    public string Vehicle { get; set; }
    public Guid SupervisorId { get; set; }
    public ISeasonEntity Season { get; set; }
    public LineType LineType { get; set; }
    public bool IsActive { get; set; }
    
    public LineEntity(Guid id, string number, DateOnly date, string vehicle, Guid supervisorId, SeasonEntity season, LineType lineType, bool isActive)
    {
        Id = id;
        Number = number;
        Date = date;
        Vehicle = vehicle;
        SupervisorId = supervisorId;
        Season = season;
        LineType = lineType;
        IsActive = isActive;
    }
}