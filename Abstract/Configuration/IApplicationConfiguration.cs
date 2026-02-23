namespace Abstract.Configuration;

public interface IApplicationConfiguration
{
    string ApplicationName { get; set; }
    bool IsWeatherEnabled { get; set; }
    bool IsRankingEnabled { get; set; }
    bool IsAnonymousRegisterEnabled { get; set; }
    string? EmailLogin { get; set; }
    string? EmailPassword { get; set; }
    int EmailSendMode { get; set; }
}