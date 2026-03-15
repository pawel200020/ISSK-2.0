using Configuration.Shared.Notifications;

namespace Configuration.Shared.Entities;

public class ApplicationConfiguration : IApplicationConfiguration
{
    public required string ApplicationName { get; set; }
    public bool IsWeatherEnabled { get; set; }
    public bool IsRankingEnabled { get; set; }
    public bool IsAnonymousRegisterEnabled { get; set; }
    public string? EmailLogin { get; set; }
    public string? EmailPassword { get; set; }
    public bool IsSelfRegisterEnabled { get; set; }
    public EmailSendMode EmailSendMode { get; set; }
    public ISmtpConfiguration? SmtpConfiguration { get; set; }
}