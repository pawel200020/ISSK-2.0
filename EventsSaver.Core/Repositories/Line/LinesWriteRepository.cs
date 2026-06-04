using Data;
using EventsSaver.Core.Entities;
using EventsSaver.Shared.Entities;

namespace EventsSaver.Core.Repositories.Line;

public class LinesWriteRepository
{
    private readonly ApplicationDbContext _context;

    public LinesWriteRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> CreateLine(LineEntity line)
    {
        _context.Lines.Add(line.ToDbEntity());
        await _context.SaveChangesAsync();
        return true;
    }
}