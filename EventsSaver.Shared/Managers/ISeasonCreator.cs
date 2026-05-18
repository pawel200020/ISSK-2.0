using EventsSaver.Shared.Entities;

namespace EventsSaver.Core.Managers;

public interface ISeasonCreator
{
    Task CreateSeason(ISeasonEntity season);
}