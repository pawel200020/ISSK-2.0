using System.Globalization;

namespace Configuration.Shared.Culture;

public interface ISupportedLanguagesDownloader
{
    Task<IEnumerable<CultureInfo>> GetSupportedLanguages();
}