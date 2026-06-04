using Data;
using EventsSaver.Core.Entities;
using EventsSaver.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsSaver.Core.Repositories.Line;

public class LinesReadRepository : ILinesReadRepository
{
    private readonly ApplicationDbContext _context;

    public LinesReadRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<ILineEntity>> GetLinesPaged(int page, int pageSize)
    {
        var linesFromDb = await _context.Lines.Skip((page - 1) * pageSize).Take(pageSize).ToArrayAsync();
        return linesFromDb.Select(l=> l.ToDomainEntity()).ToArray();
    }
    
    public async Task<int> GetLinesCount() => await _context.Lines.CountAsync();
}