using EventsSaver.Shared.Entities;

namespace EventsSaver.Shared.Managers.Seasons;

public interface ISeasonCreator
{
    Task CreateSeason(ISeasonEntity season);
}