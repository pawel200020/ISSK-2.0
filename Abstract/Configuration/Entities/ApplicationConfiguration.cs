namespace Abstract.Configuration.Entities;

public class ApplicationConfiguration : IApplicationConfiguration
{
    public required string ApplicationName { get; set; }
    public bool IsWeatherEnabled { get; set; }
    public bool IsRankingEnabled { get; set; }
    public bool IsAnonymousRegisterEnabled { get; set; }
    public string? EmailLogin { get; set; }
    public string? EmailPassword { get; set; }
    public bool IsSelfRegisterEnabled { get; set; }
    public int EmailSendMode { get; set; }
}