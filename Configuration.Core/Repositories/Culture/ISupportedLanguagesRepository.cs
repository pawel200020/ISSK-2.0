using Data.Entites.Languages;

namespace Configuration.Repositories.Culture;

internal interface ISupportedLanguagesRepository
{
    Task<IEnumerable<SupportedLanguage>> GetSupportedLanguages();
}