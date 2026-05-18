namespace EventsSaver.Shared.Managers;

public interface ISeasonRemover
{
    Task RemoveSeason(Guid seasonId);
}