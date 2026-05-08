namespace Configuration.Shared.Notifications;

public interface ISmtpConfiguration
{
    public string? SmtpServerAddress { get; set; }
    public int? SmtpServerPort { get; set; }
    public bool UseSsl { get; set; }
}