using Configuration.Shared.Notifications;

namespace Configuration.Shared;

public interface IApplicationConfiguration
{
    string ApplicationName { get; set; }
    bool IsWeatherEnabled { get; set; }
    bool IsRankingEnabled { get; set; }
    bool IsAnonymousRegisterEnabled { get; set; }
    string? EmailLogin { get; set; }
    string? EmailPassword { get; set; }
    bool IsEmailRedirect { get; set; }
    string? EmailRedirectAddress { get; set; }
    EmailSendMode EmailSendMode { get; set; }
    ISmtpConfiguration? SmtpConfiguration { get; set; }
}