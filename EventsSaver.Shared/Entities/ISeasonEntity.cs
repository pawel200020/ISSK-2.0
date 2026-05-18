namespace EventsSaver.Shared.Entities;

public interface ISeasonEntity
{
    public Guid SeasonId { get; set; }
    public string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}