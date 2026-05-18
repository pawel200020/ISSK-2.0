using Data;
using EventsSaver.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsSaver.Core.Repositories.Seasons;

internal class SeasonsReadRepository : ISeasonsReadRepository
{
    private readonly ApplicationDbContext _context;

    public SeasonsReadRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<SeasonEntity>> GetSeasonsFromDb() =>
        await _context.Seasons.Select(s => new SeasonEntity(s.Id, s.Name, s.StartDate, s.EndDate)).ToListAsync();
}