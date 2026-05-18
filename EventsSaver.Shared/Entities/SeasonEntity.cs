namespace EventsSaver.Shared.Entities;

public class SeasonEntity : ISeasonEntity
{
    public Guid SeasonId { get; set; }
    public string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public SeasonEntity(Guid seasonId, string name, DateOnly startDate, DateOnly endDate)
    {
        SeasonId = seasonId;
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
    }

    public SeasonEntity(){}
}
