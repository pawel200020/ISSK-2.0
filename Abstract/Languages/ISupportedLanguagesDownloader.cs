using System.Globalization;

namespace Abstract.Languages;

public interface ISupportedLanguagesDownloader
{
    Task<IEnumerable<CultureInfo>> GetSupportedLanguages();
}