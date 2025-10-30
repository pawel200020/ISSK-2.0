using System.Globalization;

namespace Abstract.Languages;

public interface ISupportedLanguagesDownloader
{
    IEnumerable<CultureInfo> GetSupportedLanguages();
}