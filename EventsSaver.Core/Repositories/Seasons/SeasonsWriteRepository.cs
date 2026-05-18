using Data;
using Data.Entites.EventSaver;
using EventsSaver.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsSaver.Core.Repositories.Seasons;

internal class SeasonsWriteRepository : ISeasonsWriteRepository
{
    private readonly ApplicationDbContext _context;

    public SeasonsWriteRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task CreateNewSeason(ISeasonEntity season)
    {
        var seasonDb = new SeasonDb()
        {
            Name = season.Name,
            StartDate = season.StartDate,
            EndDate = season.EndDate,
            TsInsert = DateTime.UtcNow,
            TsUpdate = DateTime.UtcNow
        };
        _context.Seasons.Add(seasonDb);
        return _context.SaveChangesAsync();
    }
    
    public async Task DeleteSeason(Guid seasonId)
    {
        await (_context.Seasons.Where(s => s.Id == seasonId)).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }
}