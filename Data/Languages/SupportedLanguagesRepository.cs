using Data.Entites.Languages;
using Microsoft.EntityFrameworkCore;

namespace Data.Languages;

internal class SupportedLanguagesRepository : ISupportedLanguagesRepository
{
    private readonly ApplicationDbContext _context;
    public SupportedLanguagesRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<IEnumerable<SupportedLanguage>>GetSupportedLanguages() 
        => await _context.SupportedLanguages.ToListAsync();
}