namespace Abstract.Configuration;

public interface IApplicationConfiguration
{
    string ApplicationName { get; set; }
    bool IsWeatherEnabled { get; set; }
    bool IsRankingEnabled { get; set; }
}