using Data.Entites.Languages;

namespace Data.Languages;

public interface ISupportedLanguagesRepository
{
    Task<IEnumerable<SupportedLanguage>> GetSupportedLanguages();
}