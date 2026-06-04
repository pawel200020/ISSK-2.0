namespace EventsSaver.Shared.Managers.Seasons;

public interface ISeasonRemover
{
    Task RemoveSeason(Guid seasonId);
}